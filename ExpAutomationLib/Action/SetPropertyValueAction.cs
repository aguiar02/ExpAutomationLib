using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;

namespace ExpAutomationLib.Action
{
	public class SetPropertyValueAction : IAction
	{
		public string PropertyExpression { get; set; }
		public string ValueExpression { get; set; }

		public void ExecuteAction()
		{

			IExpression exp = Parser.Parse(PropertyExpression);
			if (exp is PropertyAccessExpression)
			{
				IExpression expVal = Parser.Parse(ValueExpression);
				expVal.CheckSemantics(Parser.ParsingContext);


				PropertyAccessExpression paexp = (PropertyAccessExpression)exp;
				paexp.CheckSemantics(Parser.ParsingContext);

				if (paexp.ExpressionType != expVal.ExpressionType)
					throw new Exception.ActionExecutionException("Tipos incompatíveis");

				paexp.Property.SetValue(paexp.ObjectExpression.Eval(), expVal.Eval(), null);
			}
			else
			{
				throw new Exception.ActionExecutionException("Não é uma propriedade");
			}

	
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
