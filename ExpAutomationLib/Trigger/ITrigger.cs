using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using System.Windows.Forms;

namespace ExpAutomationLib.Trigger
{
	public delegate void TriggerEvent();

	public interface ITrigger
	{
		ExpressionParser Parser { get; set; }
		void SetParameters();
		//void RegisterWindowsMessaging(IList<ITrigger> registeredWinMessage);
		
		
	}
}
