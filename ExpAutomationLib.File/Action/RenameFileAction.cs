using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class RenameFileAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;

		public string ToExpression { get; set; }
		private string to;

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);
			to = ExpHelper.EvalToString(Parser, ToExpression);
			System.IO.File.Move(filePath, to);
		}


		public ExpressionParser Parser { get; set; }
	}
}
