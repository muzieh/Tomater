using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomater
{
	public class TomaterTimer
	{
		ITimer _timer;
		public CounterStatus CurrentState { get; set; }
		public TomaterTimer(ITimer timer)
		{
			_timer = timer;
		}


		public void StartWork()
		{
			RemainingTime = TimeSpan.FromMinutes(25);
			CurrentState = CounterStatus.Work;
			_timer.Start();
		}

		public TimeSpan RemainingTime { get; set; }

		public void Stop()
		{
			CurrentState = CounterStatus.Stopped;
		}
	}
}
