using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class GlobalTimer
    {
        public int Id { get; set; }
        [Required]
        public DateTimeOffset StartTime { get; set; }
        public TimeSpan WorkTimeSpan { get; set; } = TimeSpan.FromMinutes(20);
        public TimeSpan FreeTimeSpan { get; set; } = TimeSpan.FromMinutes(5);

        public TimerStateSnapshot GetCurrentState()
        {
            var left = DateTimeOffset.UtcNow - StartTime;
            var r = (int)Math.Ceiling(left / (WorkTimeSpan + FreeTimeSpan));
            var toNextEnd = StartTime.Add(r * (WorkTimeSpan + FreeTimeSpan)) - DateTimeOffset.UtcNow;
            if (toNextEnd < FreeTimeSpan)
            {
                return new TimerStateSnapshot(
                    TimerState.Free,
                    DateTimeOffset.UtcNow.Add(toNextEnd),
                    toNextEnd,
                    r);
            }
            else
            {
                return new TimerStateSnapshot(
                    TimerState.Work,
                    DateTimeOffset.UtcNow.Add(toNextEnd).Subtract(FreeTimeSpan),
                    toNextEnd - FreeTimeSpan,
                    r);
            }
        }
        public enum TimerState
        {
            Work,
            Free
        }
        public class TimerStateSnapshot
        {
            public TimerStateSnapshot(TimerState state, DateTimeOffset nextCheckpoint, TimeSpan timeToNextCheckpoint, int timerNumber)
            {
                State = state;
                NextCheckpoint = nextCheckpoint;
                TimeToNextCheckpoint = timeToNextCheckpoint;
                TimerNumber = timerNumber;
            }
            public TimerState State { get; }
            public DateTimeOffset NextCheckpoint { get;  }
            public TimeSpan TimeToNextCheckpoint { get; }
            public string TimeToNextCheckpointString => TimeToNextCheckpoint.ToString(@"hh\:mm\:ss");
            public int TimerNumber { get; }
        }
    }

}
