using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class MoveFileAction : IAction
	{
		public string SourcePathExpression { get; set; }
		private string sourcePath;

		public string DestPathExpression { get; set; }
		private string destPath;

		public void ExecuteAction()
		{
			sourcePath = ExpHelper.EvalToString(Parser, SourcePathExpression);
			destPath = ExpHelper.EvalToString(Parser, DestPathExpression);
			System.IO.File.Move(sourcePath, destPath);
		}


		public ExpressionParser Parser { get; set; }
	}
}
