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

        public GlobalTimer GetLastTimerAsync()
        {
            Timer = dbContext.GlobalTimers.FirstOrDefault();
            return Timer;
        }

        public string GetTimeLeft()
        {
            if (Timer == null)
            {
                return "No timer";
            }
            TimeSpan time = (DateTimeOffset.Now - Timer.StartTime);
            if (time.TotalSeconds < 1800 && Timer.StartTime.CompareTo(DateTimeOffset.Now) < 0)
            {
                if (time.TotalMilliseconds < 1500000)
                {
                    return $"S.{time.TotalMinutes}:{time.TotalSeconds}:{time.TotalMilliseconds}";
                }
                else
                {
                    return $"F.{time.TotalMinutes}:{time.TotalSeconds}:{time.TotalMilliseconds}";
                }
            }
            else
            {
                if (Timer.StartTime.CompareTo(DateTimeOffset.Now) > 0)
                {
                    return "Not started";
                } else
                {
                    return "Finished";
                }
            }
        }
    }
}
