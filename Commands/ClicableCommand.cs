using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Tomater.Commands
{
	abstract public class ClicableCommand
	{
		public ClicableCommand(Button button)
		{
			button.Click += this.ButtonAction;
		}

		protected abstract void ButtonAction(object button, EventArgs args);
	}
}
