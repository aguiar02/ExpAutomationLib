using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;
using ExpAutomationLib.Exception;
using System.Collections;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class ForAction : IHasInnerAction
	{
		public string ItemVarName { get; set; }
		public string CollectionExpression { get; set; }
		public IAction InnerAction { get; set; }

		public void ExecuteAction()
		{
			InnerAction.Parser = Parser;
			IExpression exp = Parser.Parse(CollectionExpression);
			exp.CheckSemantics(Parser.ParsingContext);
			var t = exp.ExpressionType;
			IEnumerable val = exp.Eval() as IEnumerable;

			if (val == null)
				throw new ActionExecutionException("Expressão não é do tipo collection.");

			Type collectionType = val.GetType();
			Type elementType = collectionType.GetGenericArguments()[0];

			foreach (var item in val)
			{
				Parameter p = Parameter.NewParameter(elementType);
				p.ParameterValue = item;
				Parser.ParsingContext.Parameters[ItemVarName] = p;
				InnerAction.ExecuteAction();
			}
			Parser.ParsingContext.Parameters.Remove(ItemVarName);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
