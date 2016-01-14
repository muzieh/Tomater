﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

		public enum TimerState {Idle, Working, ShortBreak}
		DateTime _endTime;
		DispatcherTimer _timer;
		SoundPlayer _soundPlayer;
		TomaterTimer _tomater;
		TimerState _currentState;

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
		ShortBreakCommand _shortBreakCommand;

		public MainWindow()
		{
			InitializeComponent();
			_currentState = TimerState.Idle;
			this.Loaded += new RoutedEventHandler(Window_Loaded);
			this.MouseLeftButtonDown += MainWindow_MouseLeftButtonDown;

			_startCommand = new StartCommand(WorkButton, this);
			_shortBreakCommand = new ShortBreakCommand(ShortBreakButton, this);

			_soundPlayer = new SoundPlayer();
			_timer = new DispatcherTimer();
			_timer.Tick += new EventHandler(TimerTick);
			_timer.Interval = new TimeSpan(0, 0, 1);
			this.ResetTimer();

		}


		private void ResetTimer()
		{
			_timer.Stop();
			TextRemainingTime.Text = "00:00";
			_currentState = TimerState.Idle; ;
			progressBar.Value = 0;
			progressBar.Maximum = 100;
		}

		void MainWindow_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			DragMove();
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
				ProcessTimerTick();
			}
			else
			{
				TextRemainingTime.Foreground = Brushes.Black;
				TextRemainingTime.Text = FormatTime(delta);
				progressBar.Value = progressBar.Maximum - delta.TotalSeconds;
			}
		}

		async Task ProcessTimerTick()
		{
			_timer.Stop();
			this.FinishBell();
			await Task.Delay(3000);
			switch(_currentState)
			{
				case TimerState.Working:
					this.ShortBreak();
					break;
				case TimerState.ShortBreak:
					this.Work();
					break;
				default:
					throw new Exception("not supported state");
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
			progressBar.Maximum = 25 * 60;
			progressBar.Value = 0;
			progressBar.Foreground = Brushes.Green;
			_currentState = TimerState.Working;
			StartBell();
			_timer.Start();
		}

		private void ButtonLongClick(object sender, RoutedEventArgs e)
		{
			_endTime = DateTime.Now.AddMinutes(20).AddSeconds(5);
			TextRemainingTime.Text = "00:00";
			StartBell();
			_timer.Start();
		}

		public void ShortBreak()
		{
			_endTime = DateTime.Now.AddMinutes(5).AddSeconds(5);
			TextRemainingTime.Text = "00:00";
			StartBell();
			progressBar.Maximum = 5 * 60;
			progressBar.Value = 0;
			progressBar.Foreground = Brushes.Yellow;
			_currentState = TimerState.ShortBreak;
			_timer.Start();
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
