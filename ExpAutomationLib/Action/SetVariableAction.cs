using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using ExpAutomationLib.Exception;
using ExpCalculatorLib.Expression;

namespace ExpAutomationLib.Action
{
	public class SetVariableAction : IAction
	{
		public void ExecuteAction()
		{
			try
			{
				IExpression exp = Parser.Parse(Expression);
				exp.CheckSemantics(Parser.ParsingContext);

				if (!Parser.ParsingContext.Parameters.ContainsKey(VariableName))
				{
					Parameter p = Parameter.NewParameter(exp.ExpressionType);
					Parser.ParsingContext.Parameters.Add(VariableName, p);
				}
					
				Parser.ParsingContext.Parameters[VariableName].ParameterValue = exp.Eval();

			}
			catch (System.Exception e)
			{
				throw new ActionExecutionException("Erro ao executar ação.", e);
			}
		}

		public string VariableName { get; set; }

		public string Expression { get; set; }

		public ExpressionParser Parser { get; set; }
	}
}
