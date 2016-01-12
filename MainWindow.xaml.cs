﻿using System;
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
			
			_startCommand = new StartCommand(WorkButton, this);
			_soundPlayer = new SoundPlayer();
			_timer = new DispatcherTimer();
			_timer.Tick += new EventHandler(timer_Tick);
			_timer.Interval = new TimeSpan(0, 0, 1);
			_timer.Stop();
			buttonVoid.IsEnabled = false;
		}

		public MainWindow(ITimer tomater)
		{
			// TODO: Complete member initialization
			InitializeComponent();
			_tomater = new TomaterTimer(tomater);
			//TextRemainingTime.Text = _tomater.RemainingTime.ToString();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			var delta = _endTime.Subtract(DateTime.Now);

			if (delta.Seconds < 0 || delta.Minutes < 0)
			{
				TextRemainingTime.Foreground = Brushes.Red;
				TextRemainingTime.Text = "-" + formatTime(delta);
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
				TextRemainingTime.Text = formatTime(delta);
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

		private string formatTime (TimeSpan span) {
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
