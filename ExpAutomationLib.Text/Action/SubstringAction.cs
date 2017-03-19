using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib.File.Action
{
	public class SubstringAction : IAction
	{
		public string InputExpression { get; set; }
		private string input;

		public string StartIndexExpression { get; set; }
		private int startIndex;

		public string LengthExpression { get; set; }
		private int length;

		public string VarName { get; set; }

		public void ExecuteAction()
		{
			input = ExpHelper.EvalToString(Parser, InputExpression);
			startIndex = ExpHelper.EvalToInt(Parser, StartIndexExpression);
			
			string text = null;

			if (!string.IsNullOrEmpty(LengthExpression))
			{
				length = ExpHelper.EvalToInt(Parser, LengthExpression);
				text = input.Substring(startIndex, length);
			}
			else
				text = input.Substring(startIndex);

			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(string)));

			if (Parser.ParsingContext.Parameters[VarName].ParameterType != typeof(string))
				throw new ActionExecutionException(string.Format("A variável {0} não é do tipo string", VarName));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = text;

		}

		public ExpressionParser Parser { get; set; }
	}
}
