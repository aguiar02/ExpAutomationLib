using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib.Action
{
	public class IfAction : IAction
	{
		public string ConditionExpression { get; set; }
		public IAction IfTrueAction { get; set; }
		public IAction IfFalseAction { get; set; }

		public void ExecuteAction()
		{
			IfTrueAction.Parser = Parser;
			IExpression exp = Parser.Parse(ConditionExpression);
			exp.CheckSemantics(Parser.ParsingContext);
			if (exp.ExpressionType != typeof(bool))
				throw new ActionExecutionException("Expressão da condição não é do tipo boolean.");

			bool val = (bool)exp.Eval();
			if (val)
			{
				IfTrueAction.ExecuteAction();
			}
			else if (IfFalseAction != null)
			{
				IfFalseAction.ExecuteAction();
			}
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
