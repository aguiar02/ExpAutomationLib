using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.Task
{
	public interface ITask
	{
		void ExecuteTask();
		IList<IAction> ActionList { get; set; }
		ExpressionParser Parser { get; set; }
	}
}
