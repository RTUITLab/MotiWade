using Database;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using RealityShiftLearning.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services
{
    public class TimerService
    {
        private LearnDbContext dbContext;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILogger<TimerService> logger;
        private TomatoTimer Timer;
        public TimerService(
            LearnDbContext dbContext,
            AuthenticationStateProvider authenticationStateProvider,
            ILogger<TimerService> logger)
        {
            this.dbContext = dbContext;
            this.authenticationStateProvider = authenticationStateProvider;
            this.logger = logger;
        }

        public async Task<TomatoTimer> GetMyCurrentTimer()
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            Timer = dbContext.GlobalTimers
                .Where(t => t.UserToExercise.UserId == state.User.UserId())
                .FirstOrDefault();
            return Timer;
        }
        // TODO check existing timers
        public async Task<TomatoTimer> CreateTimerForMe(int exerciseId)
        {
            logger.LogWarning("existing timers don't checked");
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            var userToExercise = await dbContext.UserToExercises
                .Where(ute => ute.ExerciseId == exerciseId)
                .Where(ute => ute.UserId == state.User.UserId())
                .FirstOrDefaultAsync();

            userToExercise.ExerciseProgress = ExerciseProgress.InProgress;

            var timer = new TomatoTimer
            {
                StartTime = DateTimeOffset.UtcNow,
                UserToExerciseId = userToExercise.Id
            };

            dbContext.GlobalTimers.Add(timer);
            await dbContext.SaveChangesAsync();
            return timer;
        }

        public async Task DoneExercise(int exerciseId)
        {
            var state = await authenticationStateProvider.GetAuthenticationStateAsync();
            var userToExercise = await dbContext.UserToExercises
                .Where(ute => ute.ExerciseId == exerciseId)
                .Where(ute => ute.UserId == state.User.UserId())
                .FirstOrDefaultAsync();
            userToExercise.FinishTime = DateTimeOffset.UtcNow;
            userToExercise.ExerciseProgress = ExerciseProgress.Done;
            await dbContext.SaveChangesAsync();
        }

        public Task<TomatoTimer> SkipWork(TomatoTimer timer) => SkipCheckPoint(timer);
        public Task<TomatoTimer> SkipFree(TomatoTimer timer) => SkipCheckPoint(timer);

        private async Task<TomatoTimer> SkipCheckPoint(TomatoTimer timer)
        {
            var state = timer.GetCurrentState();
            timer.StartTime -= state.TimeToNextCheckpoint;
            dbContext.GlobalTimers.Update(timer);
            await dbContext.SaveChangesAsync();
            return timer;
        }
    }
}
