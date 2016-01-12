using System;
using System.Linq;
using Tomater;

namespace TomaterTests
{
	class MockTimer : ITimer
	{


		public void Start()
		{
		}

		public void Stop()
		{
		}

		public void KickTick(TimeSpan timeSpan)
		{
			if (Tick != null)
			{
				Tick(this, new TimerEventArgs(timeSpan));
			}
		}

		public event TimerEventHandler Tick;
		
	}
}
