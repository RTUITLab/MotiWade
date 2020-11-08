using Database;
using Google.Apis.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services.Hosted
{
    public class TomatoCheckPointPushUpdater : BackgroundService
    {
        private readonly ILogger<TomatoCheckPointPushUpdater> logger;
        private readonly IServiceScopeFactory scopeFactory;
        private ConcurrentDictionary<int, TomatoTimer.TimerState> progresses = new ConcurrentDictionary<int, TomatoTimer.TimerState>();
        public TomatoCheckPointPushUpdater(
            ILogger<TomatoCheckPointPushUpdater> logger,
            IServiceScopeFactory scopeFactory)
        {
            this.logger = logger;
            this.scopeFactory = scopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                logger.LogDebug("Start iteration");
                using var scope = scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<LearnDbContext>();
                var pushService = scope.ServiceProvider.GetRequiredService<PushNotifyService>();
                var links = await db.UserToExercises
                    .Where(ute => ute.ExerciseProgress == Models.ExerciseProgress.InProgress)
                    .Include(ute => ute.TomatoTimer)
                    .Include(ute => ute.Exercise)
                    .ToListAsync();
                logger.LogDebug($"Receive {links.Count} records");
                foreach (var link in links)
                {
                    bool needUpdate = false;
                    var tomatoState = link.TomatoTimer.GetCurrentState();
                    progresses.AddOrUpdate(link.ExerciseId, tomatoState.State, (k, old) =>
                    {
                        needUpdate = old != tomatoState.State;
                        return tomatoState.State;
                    });
                    if (needUpdate)
                    {
                        logger.LogInformation($"send notify about exercise {link.ExerciseId}");
                        if (tomatoState.State == TomatoTimer.TimerState.Free)
                        {
                            await pushService.SendNotifyToUser(
                                $"It's time to have a rest!",
                                $"Why don't you walk around or drink a glass of water?",
                                link.UserId);
                        }
                        if (tomatoState.State == TomatoTimer.TimerState.Work)
                        {
                            await pushService.SendNotifyToUser(
                                $"It's time to continue!",
                                $"The way is also a part of target.In all 20 minutes to become better.",
                                link.UserId);
                        }
                    }
                }
            }
        }
    }
}
