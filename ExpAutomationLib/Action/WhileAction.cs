using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib.Action
{
	public class WhileAction : IHasInnerAction
	{
		public string ConditionExpression { get; set; }
		public IAction InnerAction { get; set; }

		public void ExecuteAction()
		{
			InnerAction.Parser = Parser;
			while (EvalCondition())
			{
				InnerAction.ExecuteAction();
			}
		}

		private bool EvalCondition()
		{
			IExpression exp = Parser.Parse(ConditionExpression);
			exp.CheckSemantics(Parser.ParsingContext);
			if (exp.ExpressionType != typeof(bool))
				throw new ActionExecutionException("Expressão da condição não é do tipo boolean.");

			return (bool)exp.Eval();
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
