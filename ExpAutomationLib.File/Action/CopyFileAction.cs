using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class CopyFileAction : IAction
	{
		public string SourcePathExpression { get; set; }
		private string sourcePath;

		public string DestPathExpression { get; set; }
		private string destPath;

		public bool Overwrite { get; set; }

		public void ExecuteAction()
		{
			sourcePath = ExpHelper.EvalToString(Parser, SourcePathExpression);
			destPath = ExpHelper.EvalToString(Parser, DestPathExpression);
			System.IO.File.Copy(sourcePath, destPath, Overwrite);
		}


		public ExpressionParser Parser { get; set; }
	}
}
