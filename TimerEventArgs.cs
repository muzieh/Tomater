using System;

namespace Tomater
{
    public class TimerEventArgs : EventArgs
    {
        public TimeSpan TimeElapsed { get; private set; }

        public TimerEventArgs(TimeSpan timeElapsed)
        {
            TimeElapsed = timeElapsed;
        }
    }
}