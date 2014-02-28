using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tomater;

namespace TomaterTests
{
	[TestFixture]
	public class MainWindowTests
	{
		[Test]
		[STAThread]
		public void CreateMainWindow_InstantCreated() {
			var timer = new MockTimer();
			var mainWindow = new MainWindow(timer);

			Assert.IsNotNull(mainWindow);
		}


		[Test]
		[STAThread]
		public void InitialDisplayTime_0000()
		{
			var timer = new MockTimer();
			var mainWindow = new MainWindow(timer);

			Assert.IsNotNull(mainWindow.TextRemainingTime);

		}

	}
}
