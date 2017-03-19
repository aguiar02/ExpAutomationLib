using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpAutomationLib.Action
{
	public class CopyExpressionToClipboardAction : IAction
	{
		public string TextExpression { get; set; }
		private string text;

		public void ExecuteAction()
		{
			text = ExpHelper.EvalToString(Parser, TextExpression);
			System.Windows.Forms.Clipboard.SetText(text);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
