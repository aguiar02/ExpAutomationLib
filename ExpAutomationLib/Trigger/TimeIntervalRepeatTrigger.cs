using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	public class TimeIntervalRepeatTrigger : IEventTrigger
	{
		public TimeSpan TimeIn { get; set; }
		public TimeSpan TimeOut { get; set; }
		public int RepeatIntervalInMinutes { get; set; }

		Thread triggerThread;
		bool isShuttingDown = false;

		public event TriggerEvent OnTriggerEvent;

		public TimeIntervalRepeatTrigger()
		{
			TimeIn = new TimeSpan(0, 0, 0);
			TimeOut = new TimeSpan(24, 0, 0);
			RepeatIntervalInMinutes = 1;
		}

		public void Setup()
		{
			if (TimeIn > TimeOut || (TimeOut - TimeIn).TotalMinutes < RepeatIntervalInMinutes)
				return;

			TimeSpan now = DateTime.Now.TimeOfDay;
			triggerThread = new Thread(() =>
			{
				if (now < TimeIn)
					Thread.Sleep(TimeIn - now);
				else if (now > TimeOut)
					Thread.Sleep(new TimeSpan(24, 0, 0) - (TimeIn - now));
				while (!isShuttingDown)
				{
					OnTriggerEvent.Invoke();
					now = DateTime.Now.TimeOfDay;
					if (now < TimeOut)
					{
						Thread.Sleep(new TimeSpan(0, RepeatIntervalInMinutes, 0));
					}
					else
					{
						Thread.Sleep(new TimeSpan(24, 0, 0) - (TimeIn - now));
					}
				}
			});
			triggerThread.Start();
		}

		public void ShutDown()
		{
			triggerThread.Abort();
		}

		public ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			
		}
	}
}
