using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Tomater;
using Moq;

namespace TomaterTests
{
	[TestFixture]
	public class TomaterTimerTests
	{
		private MockTimer GetMockTimer()
		{
			return new MockTimer();
		}

		[Test]
		public void CreateInstance()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			Assert.IsNotNull(tomater);
		}

		[Test]
		public void InitialState_Stopped()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			Assert.AreEqual(CounterStatus.Stopped, tomater.CurrentState);
		}

		[Test]
		public void SetToWork_InitailState_StatusWorking()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartWork();
			Assert.AreEqual(CounterStatus.Work, tomater.CurrentState);
		}

		[Test]
		public void SetToWork_InitalState_25minutesRemaining()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartWork();
			Assert.AreEqual(TimeSpan.FromMinutes(25), tomater.RemainingTime);
		}

		[Test]
		public void SetToWork_ThenStop_StatusStopped()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartWork();
			tomater.Stop();
			Assert.AreEqual(CounterStatus.Stopped, tomater.CurrentState);
		}

		[Test]
		public void SetToWork_Wait10Seconds_RemainingTime2450()
		{
			var timer = GetMockTimer();
			var tomater = new TomaterTimer(timer);
			tomater.StartWork();
			//wait ten seconds
			timer.KickTick(TimeSpan.FromSeconds(10));
			Assert.AreEqual(TimeSpan.FromSeconds(24 * 60 + 50), tomater.RemainingTime);
		}

		[Test]
		public void SetToBreak_InitailState_StatusBreak()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartBreak();
			Assert.AreEqual(CounterStatus.Break, tomater.CurrentState);
		}

		[Test]
		public void SetToLongBreak_InitailState_StatusLongBreak()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartLongBreak();
			Assert.AreEqual(CounterStatus.LongBreak, tomater.CurrentState);
		}

		[Test]
		public void SetToBreak_InitailState_5minutesRemaining()
		{
			var tomater = new TomaterTimer(GetMockTimer());
			tomater.StartBreak();
			Assert.AreEqual(TimeSpan.FromMinutes(5), tomater.RemainingTime );
		}

		[Test]
		public void SetToLongBreak_InitailState_25minutesRemaining()
		{
			var timer = GetMockTimer();
			var tomater = new TomaterTimer(timer);
			tomater.StartWork();

			Assert.AreEqual(TimeSpan.FromMinutes(25), tomater.RemainingTime );
		}

		[Test]
		public void Stop_FirstWorkingThenStopped_RemaingTimeDoesntChange()
		{
			var timerMock = new Mock<ITimer>(MockBehavior.Strict);
			timerMock.Setup(t => t.Start()).Raises(r => r.Tick += null, new TimerEventArgs(TimeSpan.FromSeconds(10)));
			timerMock.Setup(t => t.Stop());

			var tomater = new TomaterTimer(timerMock.Object);
			tomater.StartWork();
			tomater.Stop();

			timerMock.VerifyAll();
			Assert.AreEqual(TimeSpan.FromSeconds(25 * 60 - 10), tomater.RemainingTime );
		}


		[Test]
		public void Start_timerGetsStart_RemaingTimeChange()
		{
			var timerMock = new Mock<ITimer>(MockBehavior.Strict);
			timerMock.Setup(t => t.Start()).Raises(r => r.Tick += null, new TimerEventArgs(TimeSpan.FromSeconds(10)));

			var tomater = new TomaterTimer(timerMock.Object);
			tomater.StartWork();

			timerMock.VerifyAll();
		}


		public void GetElapsedTime_Working_2450After10SecondsTick()
		{
			var timer = new MockTimer();

			var tomater = new TomaterTimer(timer);
			tomater.StartWork();
			for(int i=0; i<10; i++)
				timer.KickTick(TimeSpan.FromSeconds(1));


			Assert.AreEqual(TimeSpan.FromSeconds(25 * 60 - 10), tomater.RemainingTime);
		}

	}
}
