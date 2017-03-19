using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Action
{
	public class ShowMessageAction : IAction
	{
		public string MessageExpression { get; set; }
		string message;

		public void ExecuteAction()
		{
			message = ExpHelper.EvalToString(Parser, MessageExpression);

			System.Windows.Forms.MessageBox.Show(message);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
