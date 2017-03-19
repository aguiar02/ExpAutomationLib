using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;
using ExpAutomationLib.Exception;
using System.Collections;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class ForEachFileAction : IHasInnerAction
	{
		public string ItemVarName { get; set; }

		public string PathExpression { get; set; }
		private string path;

		public string Filter { get; set; }

		public IAction InnerAction { get; set; }

		public void ExecuteAction()
		{
			InnerAction.Parser = Parser;

			path = ExpHelper.EvalToString(Parser, PathExpression);

			foreach (var item in System.IO.Directory.EnumerateFiles(path, Filter))
			{
				Parser.ParsingContext.Parameters[ItemVarName] = Parameter.NewParameter(item);
				InnerAction.ExecuteAction();
			}
			Parser.ParsingContext.Parameters.Remove(ItemVarName);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
