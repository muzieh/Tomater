using System;
using System.Linq;

namespace Tomater
{
    public interface ITimer
	{
		void Start();
		void Stop();
		event TimerEventHandler Tick;
	}
}
