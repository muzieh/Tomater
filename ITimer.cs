using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

	public delegate void TimerEventHandler(object sender, TimerEventArgs e);

	public interface ITimer
	{
		void Start();
		void Stop();
		event TimerEventHandler Tick;
	}
}
