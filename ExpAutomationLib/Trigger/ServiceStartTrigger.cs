using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	public class ServiceStartTrigger : IEventTrigger
	{
		public event TriggerEvent OnTriggerEvent;

		Thread triggerThread;
		bool isShuttingDown = false;

		public void Setup()
		{
			triggerThread = new Thread(() =>
			{
				Thread.Sleep(new TimeSpan(0, 0, 0, 500));
				OnTriggerEvent.Invoke();
			});
			triggerThread.Start();
		}

		public void ShutDown()
		{
			
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			
		}
	}
}
