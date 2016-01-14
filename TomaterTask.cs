using System;
using System.Linq;

namespace Tomater
{
	public class TomaterTask
	{
		public TomaterTask()
		{
			Id = Guid.NewGuid();
			Name = "<TODO>";
		}

		public TomaterTask(Guid guid, string name)
		{
			Id = guid;
			Name = name;
		}

		public TomaterTask(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}

		public Guid Id {get;set;}
		public string Name { get; set; }
	}
}
