using System;
using System.Linq;

namespace Tomater
{
	public class RealTimer : ITimer
	{
		public void Start()
		{
			throw new NotImplementedException();
		}

		public void Stop()
		{
			throw new NotImplementedException();
		}

		public event TimerEventHandler Tick;
	}
}
