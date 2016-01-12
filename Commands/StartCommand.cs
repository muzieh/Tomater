using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Tomater.Commands
{
	public class StartCommand : ClicableCommand
	{
		private MainWindow app;

		public StartCommand(Button button, MainWindow app)
			:base(button)
		{
			this.app = app;
		}

		protected override void ButtonAction(object button, EventArgs args)
		{
			app.Work();
		}
	}
}
