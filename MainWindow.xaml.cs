using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using System.Media;
using Tomater.Commands;

namespace Tomater
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DateTime _endTime;
		DispatcherTimer _timer;
		SoundPlayer _soundPlayer;
		TomaterTimer _tomater;

		private bool _working;
		private bool Working {
			get
			{
				return _working;
			}
			set
			{
				_working = value;
				buttonVoid.IsEnabled = value;
			}
		}
		private int _finished = 0;
		private int Finished
		{
			get
			{
				return _finished;
			}
			set
			{
				_finished = value;
				this.Title =  "Tomater + " + _finished;
			}
		}

		StartCommand _startCommand;

		public MainWindow()
		{
			InitializeComponent();
			this.Loaded += new RoutedEventHandler(Window_Loaded);
			this.MouseMove += MainWindow_MouseMove;
			this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;
			this.MouseLeftButtonUp += MainWindow_MouseLeftButtonUp;
			_startCommand = new StartCommand(WorkButton, this);
			_soundPlayer = new SoundPlayer();
			_timer = new DispatcherTimer();
			_timer.Tick += new EventHandler(TimerTick);
			_timer.Interval = new TimeSpan(0, 0, 1);
			_timer.Stop();
			buttonVoid.IsEnabled = false;
		}

		public bool dragAction = false;

		void MainWindow_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			dragAction = false;

		}

		void MainWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			dragAction = true;
		}

		void MainWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if(dragAction)
			{
				var position = e.GetPosition(this.WorkButton);
				this.Left += position.X;
				this.Top += position.Y;
			}
		}
 
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
			this.Left = desktopWorkingArea.Right - this.Width;
			this.Top = desktopWorkingArea.Bottom - this.Height;
		}

		public MainWindow(ITimer tomater)
		{
			// TODO: Complete member initialization
			InitializeComponent();
			_tomater = new TomaterTimer(tomater);
			//TextRemainingTime.Text = _tomater.RemainingTime.ToString();
		}

		void TimerTick(object sender, EventArgs e)
		{
			var delta = _endTime.Subtract(DateTime.Now);

			if (delta.Seconds < 0 || delta.Minutes < 0)
			{
				TextRemainingTime.Foreground = Brushes.Red;
				TextRemainingTime.Text = "-" + FormatTime(delta);
				if (Working)
				{
					Finished++;
					Working = false;
					FinishBell();
				}
				
			}
			else
			{
				TextRemainingTime.Foreground = Brushes.Black;
				TextRemainingTime.Text = FormatTime(delta);
			}
		}

		private void FinishBell()
		{
			_soundPlayer.SoundLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EndBell.wav");
			_soundPlayer.Play();
		}

		private void StartBell()
		{
			_soundPlayer.SoundLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"StartBell.wav");
			_soundPlayer.Play();
		}

		private string FormatTime (TimeSpan span) {
			return span.ToString("mm") + ":" + span.ToString("ss");			
		}

		public void Work()
		{
			_endTime = DateTime.Now.AddMinutes(25).AddSeconds(5);
			TextRemainingTime.Text = "00:00";
			Working = true;
			StartBell();
			_timer.Start();
		}

		private void ButtonLongClick(object sender, RoutedEventArgs e)
		{
			_endTime = DateTime.Now.AddMinutes(20).AddSeconds(5);
			TextRemainingTime.Text = "00:00";
			Working = false;
			StartBell();
			_timer.Start();
		}

		private void ButtonShortClick(object sender, RoutedEventArgs e)
		{
			_endTime = DateTime.Now.AddMinutes(5).AddSeconds(5);
			TextRemainingTime.Text = "00:00";
			Working = false;
			StartBell();
			_timer.Start();
		}

		private void ButtonVoidClick(object sender, RoutedEventArgs e)
		{
			_timer.Stop();
			TextRemainingTime.Text = "00:00";
			Working = false;
			
		}


		private void ButtonTasksClick(object sender, RoutedEventArgs e)
		{
			Tasks tasks = new Tasks();
			tasks.Show();
		}
	}
}
