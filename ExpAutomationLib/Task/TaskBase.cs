using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Task
{
	[Serializable]
	public abstract class TaskBase : ITask
	{
		public TaskBase()
		{
			Parser = ExpressionParser.CreateParser();
		}

		public void ExecuteTask()
		{
			foreach (var action in ActionList)
			{
				action.Parser = Parser;
				action.ExecuteAction();
			}
		}

		public IList<IAction> ActionList { get; set; }

		public ExpressionParser Parser { get; set; }
	}
}
