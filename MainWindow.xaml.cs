using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Tomater
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DateTime endTime;
		DispatcherTimer timer;

		public MainWindow()
		{
			InitializeComponent();
			timer = new DispatcherTimer();
			timer.Tick += new EventHandler(timer_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Stop();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			var delta = endTime.Subtract(DateTime.Now);

			if (delta.Seconds < 0 || delta.Minutes < 0)
			{
				textDateDisplay.Foreground = Brushes.Red;
				textDateDisplay.Text = "- " + formatTime(delta);
			}
			else
			{
				textDateDisplay.Foreground = Brushes.Black;
				textDateDisplay.Text = formatTime(delta);
			}
		}

		private string formatTime (TimeSpan span) {
			return span.ToString("mm") + ":" + span.ToString("ss");			
		}

		private void buttonWork_Click(object sender, RoutedEventArgs e)
		{
			endTime = DateTime.Now.AddMinutes(25).AddSeconds(5);
			textDateDisplay.Text = "00:00";
			timer.Start();
		}

		private void buttonLong_Click(object sender, RoutedEventArgs e)
		{
			endTime = DateTime.Now.AddMinutes(20).AddSeconds(5);
			textDateDisplay.Text = "00:00";
			timer.Start();
		}

		private void buttonShort_Click(object sender, RoutedEventArgs e)
		{
			endTime = DateTime.Now.AddMinutes(5).AddSeconds(5);
			textDateDisplay.Text = "00:00";
			timer.Start();
		}
	}
}
