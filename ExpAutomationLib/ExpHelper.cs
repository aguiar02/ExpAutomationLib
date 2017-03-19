using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using ExpCalculatorLib.Expression;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib
{
	public class ExpHelper
	{
		public static string EvalToString(ExpressionParser parser, string expression)
		{
			try
			{
				IExpression exp = parser.Parse(expression + '$');
				exp.CheckSemantics(parser.ParsingContext);
				Type type = exp.ExpressionType;
				return Convert.ToString(exp.Eval());
			}
			catch (System.Exception)
			{
				return expression;
			}
		}

		
		public static int EvalToInt(ExpressionParser parser, string expression)
		{
			try
			{
				IExpression exp = parser.Parse(expression + '$');
				exp.CheckSemantics(parser.ParsingContext);
				Type type = exp.ExpressionType;
				return Convert.ToInt32(exp.Eval());
			}
			catch (System.Exception)
			{
				throw new ActionExecutionException("Expressão não do tipo int:" + expression);
			}
		}
	}
}
