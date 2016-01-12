using System;
using System.Linq;
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


        //[Test]
        //[STAThread]
        //public void InitialDisplayTime_0000()
        //{
        //    var timer = new MockTimer();
        //    var mainWindow = new MainWindow(timer);

        //    Assert.IsNotNull(mainWindow.TextRemainingTime);

        //}

	}
}
