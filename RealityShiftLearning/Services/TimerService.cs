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
        private GlobalTimer Timer;
        public TimerService(LearnDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public GlobalTimer GetMyCurrentTimer()
        {
            Timer = dbContext.GlobalTimers
                .FirstOrDefault();
            return Timer;
        }
        public async Task<GlobalTimer> CreateTimerForMe()
        {
            var timer = new GlobalTimer
            {
                StartTime = DateTimeOffset.UtcNow
            };
            dbContext.GlobalTimers.Add(timer);
            await dbContext.SaveChangesAsync();
            return timer;
        }
    }
}
