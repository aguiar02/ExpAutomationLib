using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;
using ExpCalculatorLib;

namespace ExpAutomationLib.File.Action
{
	public class DeleteFileAction : IAction
	{
		public string PathExpression { get; set; }
		private string path;

		public void ExecuteAction()
		{
			path = ExpHelper.EvalToString(Parser, PathExpression);
			System.IO.File.Delete(path);
		}


		public ExpressionParser Parser { get; set; }
	}
}
