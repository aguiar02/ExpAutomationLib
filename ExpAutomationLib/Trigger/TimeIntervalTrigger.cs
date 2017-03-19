using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Trigger
{
	public class TimeIntervalTrigger : IStateTrigger
	{
		public DateTime DateIn { get; set; }
		public DateTime DateOut { get; set; }

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public bool IsStateActive
		{
			get 
			{
				DateTime now = DateTime.Now;
				return now.Hour <= DateOut.Hour && now.Hour <= DateIn.Hour;
			}
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
