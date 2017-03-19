using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class SimpleAction : IAction
	{

		public void ExecuteAction()
		{
			
		}

		public ExpressionParser Parser { get; set; }
	}
}
