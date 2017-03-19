using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	/// <summary>
	/// Event trigger default para o caso de um profile só possuir state triggers
	/// </summary>
	public class DefaultTrigger : IEventTrigger
	{
		public event TriggerEvent OnTriggerEvent;
		public event TriggerEvent OnExitTriggerEvent;

		Thread triggerThread;
		bool isShuttingDown = false;

		public void Setup()
		{
			triggerThread = new Thread(() =>
			{
				while (!isShuttingDown)
				{
					Thread.Sleep(new TimeSpan(0, 0, 0, 500));
					OnTriggerEvent.Invoke();
				}
			});
			triggerThread.Start();
		}


		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			
		}


		public void ShutDown()
		{
			isShuttingDown = true;
			triggerThread.Join();
		}
	}
}
