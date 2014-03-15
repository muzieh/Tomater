using System;
using System.Linq;
using System.Reflection;

namespace TomaterTests
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			string[] myArgs = { Assembly.GetExecutingAssembly().Location };

			int returnCode = NUnit.ConsoleRunner.Runner.Main(myArgs);

			if (returnCode != 0)
				Console.Beep();

			Console.ReadKey();
		}
	}
}
