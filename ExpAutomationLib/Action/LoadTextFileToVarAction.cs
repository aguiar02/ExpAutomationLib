using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class LoadTextFileToVarAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;
		
		public string VarName { get; set; }

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);
			string[] lines = File.ReadAllLines(filePath);
			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(IEnumerable<string>)));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = lines;

		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
