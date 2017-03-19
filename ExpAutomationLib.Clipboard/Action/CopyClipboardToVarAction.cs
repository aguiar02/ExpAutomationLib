using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using System.Windows.Forms;

namespace ExpAutomationLib.Action
{
	public class CopyClipboardToVarAction : IAction
	{
		public string VarName { get; set; }

		public void ExecuteAction()
		{
			if (!Parser.ParsingContext.Parameters.ContainsKey(VarName))
				Parser.ParsingContext.Parameters.Add(VarName, Parameter.NewParameter(typeof(string)));

			Parser.ParsingContext.Parameters[VarName].ParameterValue = System.Windows.Forms.Clipboard.GetText();
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
