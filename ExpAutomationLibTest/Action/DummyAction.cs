using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLibTest.Action
{
	internal class DummyAction : ExpAutomationLib.Action.IAction
	{
		public int Count { get; set; }
		public DummyAction()
		{
			Count = 0;
		}

		public void ExecuteAction()
		{
			Count++;
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
