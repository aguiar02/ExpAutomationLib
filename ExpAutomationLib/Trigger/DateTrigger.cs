using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	public class DateTrigger : IStateTrigger
	{
		public DateTime Date { get; set; }

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public bool IsStateActive 
		{
			get { return DateTime.Today.Equals(Date.Date); }
		}

		public void ExecuteMonitor()
		{

		}


		public void SetParameters()
		{
			
		}


		public void ShutDown()
		{
			
		}
	}
}
