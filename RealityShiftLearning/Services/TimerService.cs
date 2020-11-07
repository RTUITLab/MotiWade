using Database;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealityShiftLearning.Services
{
    public class TimerService
    {
        private LearnDbContext dbContext;
        private TomatoTimer Timer;
        public TimerService(LearnDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public TomatoTimer GetMyCurrentTimer()
        {
            Timer = dbContext.GlobalTimers
                .FirstOrDefault();
            return Timer;
        }
        public async Task<TomatoTimer> CreateTimerForMe()
        {
            var timer = new TomatoTimer
            {
                StartTime = DateTimeOffset.UtcNow
            };
            dbContext.GlobalTimers.Add(timer);
            await dbContext.SaveChangesAsync();
            return timer;
        }
    }
}
