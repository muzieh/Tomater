using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

		public event EventHandler Tick;
	}
}
