using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public interface IAction
	{
		void ExecuteAction();
		//ParsingContext Context { get; set; }
		ExpressionParser Parser { get; set; }
	}
}
