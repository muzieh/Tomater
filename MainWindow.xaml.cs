using System;
using System.Threading.Tasks;
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
		private enum TimerState {Idle, Working, ShortBreak}
		private DateTime endTime;
		private readonly DispatcherTimer timer;
		private readonly SoundPlayer soundPlayer;
		TomaterTimer tomater;
		private TimerState currentState;

		private ShortBreakCommand shortBreakCommand;

		public MainWindow()
		{
			this.InitializeComponent();
			this.currentState = TimerState.Idle;
			this.Loaded += new RoutedEventHandler(this.Window_Loaded);
			this.MouseLeftButtonDown += this.MainWindow_MouseLeftButtonDown;
			this.TextRemainingTime.MouseLeftButtonDown += this.TextRemainingTime_MouseLeftButtonDown;
			new StartCommand(this.WorkButton, this);
			this.shortBreakCommand = new ShortBreakCommand(this.ShortBreakButton, this);

			this.soundPlayer = new SoundPlayer();
			this.timer = new DispatcherTimer();
			this.timer.Tick += new EventHandler(this.TimerTick);
			this.timer.Interval = new TimeSpan(0, 0, 1);
			this.ResetTimer();

		}

		void TextRemainingTime_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			;
		}


		private void ResetTimer()
		{
			this.timer.Stop();
			this.StopTickingSound();
			this.TextRemainingTime.Text = "00:00";
			this.currentState = TimerState.Idle; ;
			this.progressBar.Value = 0;
			this.progressBar.Maximum = 100;
		}

		void MainWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			this.DragMove();
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
			this.InitializeComponent();
			this.tomater = new TomaterTimer(tomater);
			//TextRemainingTime.Text = _tomater.RemainingTime.ToString();
		}

		void TimerTick(object sender, EventArgs e)
		{
			var delta = this.endTime.Subtract(DateTime.Now);

			if (delta.Seconds < 0 || delta.Minutes < 0)
			{
				this.ProcessTimerTick();
			}
			else
			{
				this.TextRemainingTime.Foreground = Brushes.Black;
				this.TextRemainingTime.Text = this.FormatTime(delta);
				this.progressBar.Value = this.progressBar.Maximum - delta.TotalSeconds;
			}
		}

		async Task ProcessTimerTick()
		{
			this.timer.Stop();
			this.FinishBell();
			await Task.Delay(3000);
			switch(this.currentState)
			{
				case TimerState.Working:
					this.ShortBreak();
					break;
				case TimerState.ShortBreak:
					this.Work();
					break;
				case TimerState.Idle:
					break;
				default:
					throw new Exception("not supported state");
			}
		}

		private void FinishBell()
		{
			this.soundPlayer.SoundLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"EndBell.wav");
			this.soundPlayer.Play();
		}

		private void StartBell()
		{
			this.soundPlayer.SoundLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"StartBell.wav");
			this.soundPlayer.Play();
		}

		private void StartTickingSound()
		{
			this.soundPlayer.SoundLocation = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"tickingSound.wav");
			this.soundPlayer.PlayLooping();
		}

		private void StopTickingSound()
		{
			this.soundPlayer.Stop();
		}

		private string FormatTime (TimeSpan span) {
			return span.ToString("mm") + ":" + span.ToString("ss");
		}

		public void Work()
		{
			int workMinutes = 25;
			this.endTime = DateTime.Now.AddMinutes(workMinutes).AddSeconds(5);
			this.TextRemainingTime.Text = "00:00";
			this.progressBar.Maximum = workMinutes * 60;
			this.progressBar.Value = 0;
			this.progressBar.Foreground = Brushes.Green;
			this.currentState = TimerState.Working;
			this.StartTickingSound();
			this.timer.Start();
		}

		private void ButtonLongClick(object sender, RoutedEventArgs e)
		{
			this.endTime = DateTime.Now.AddMinutes(20).AddSeconds(5);
			this.TextRemainingTime.Text = "00:00";
			this.StartBell();
			this.timer.Start();
		}

		public void ShortBreak()
		{
			this.endTime = DateTime.Now.AddMinutes(5).AddSeconds(5);
			this.TextRemainingTime.Text = "00:00";
			this.StartBell();
			this.progressBar.Maximum = 5 * 60;
			this.progressBar.Value = 0;
			this.progressBar.Foreground = Brushes.Yellow;
			this.currentState = TimerState.ShortBreak;
			this.timer.Start();
		}

		private void ButtonVoidClick(object sender, RoutedEventArgs e)
		{
			this.ResetTimer();
		}

		private void ButtonTasksClick(object sender, RoutedEventArgs e)
		{
			Tasks tasks = new Tasks();
			tasks.Show();
		}
	}
}
