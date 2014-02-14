using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tomater
{
	public interface ITimer
	{
		void Start();
		void Stop();
		event EventHandler Tick;
	}
}
