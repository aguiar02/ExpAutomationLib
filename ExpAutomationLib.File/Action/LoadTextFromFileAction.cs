using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExpCalculatorLib;
using ExpAutomationLib.Action;
using ExpAutomationLib.Exception;

namespace ExpAutomationLib.File.Action
{
	public class LoadTextFromFileAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;
		
		public string VarName { get; set; }

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);
			string text = System.IO.File.ReadAllText(filePath);

			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(string)));

			if (Parser.ParsingContext.Parameters[VarName].ParameterType != typeof(string))
				throw new ActionExecutionException(string.Format("A variável {0} não é do tipo string", VarName));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = text;

		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
