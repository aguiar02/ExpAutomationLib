using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class AppendToFileAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;

		public string ContentExpression { get; set; }
		private string content;

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);
			content = ExpHelper.EvalToString(Parser, ContentExpression);
			System.IO.File.AppendAllText(filePath, content);
		}


		public ExpressionParser Parser { get; set; }
	}
}
