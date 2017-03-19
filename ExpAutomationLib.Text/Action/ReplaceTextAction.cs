using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib.File.Action
{
	public class ReplaceTextAction : IAction
	{
		public string InputExpression { get; set; }
		private string input;

		public string Pattern { get; set; }

		public string Replacement { get; set; }

		public string VarName { get; set; }

		public void ExecuteAction()
		{
			input = ExpHelper.EvalToString(Parser, InputExpression);

			string text = System.Text.RegularExpressions.Regex.Replace(input, Pattern, Replacement, System.Text.RegularExpressions.RegexOptions.ECMAScript);

			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(string)));

			if (Parser.ParsingContext.Parameters[VarName].ParameterType != typeof(string))
				throw new ActionExecutionException(string.Format("A variável {0} não é do tipo string", VarName));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = text;

		}

		public ExpressionParser Parser { get; set; }
	}
}
