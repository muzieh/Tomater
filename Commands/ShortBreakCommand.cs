using System;
using System.Windows.Controls;

namespace Tomater.Commands
{
	public class ShortBreakCommand : ClicableCommand
	{
		private readonly MainWindow app;

		public ShortBreakCommand(Button button, MainWindow app)
			: base(button)
		{
			this.app = app;
		}

		protected override void ButtonAction(object button, EventArgs args)
		{
			app.ShortBreak();
		}
	}
}