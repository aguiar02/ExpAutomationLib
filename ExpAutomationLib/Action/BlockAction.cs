using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class BlockAction : IAction
	{
		public IList<IAction> ActionList { get; set; }
		public ExpressionParser Parser { get; set; }

		public void ExecuteAction()
		{
			foreach (var action in ActionList)
			{
				action.Parser = Parser;
				action.ExecuteAction();
			}
		}

	}
}
