using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
