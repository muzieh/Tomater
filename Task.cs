using System;
using System.Linq;

namespace Tomater
{
	public class Task
	{
		public Task()
		{
			Id = Guid.NewGuid();
			Name = "<TODO>";
		}

		public Task(Guid guid, string name)
		{
			Id = guid;
			Name = name;
		}

		public Task(string name)
		{
			Id = Guid.NewGuid();
			Name = name;
		}

		public Guid Id {get;set;}
		public string Name { get; set; }
	}
}
