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
	public class LoadLinesFromFileAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;
		
		public string VarName { get; set; }

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);
			string[] lines = System.IO.File.ReadAllLines(filePath);
			
			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(IEnumerable<string>)));

			if (Parser.ParsingContext.Parameters[VarName].ParameterType != typeof(IEnumerable<string>))
				throw new ActionExecutionException(string.Format("A variável {0} não é uma coleção de string", VarName));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = lines;

		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
