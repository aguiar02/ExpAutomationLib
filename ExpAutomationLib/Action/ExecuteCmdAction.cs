using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class ExecuteCmdAction : IAction
	{
		public string CommandTextExpression { get; set; }
		private string commandText;
		public bool HideWindow { get; set; }
		public string OutputToVar { get; set; }

		public void ExecuteAction()
		{
			commandText = ExpHelper.EvalToString(Parser, CommandTextExpression);
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			if (HideWindow)
				startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

			startInfo.FileName = "cmd.exe";
			startInfo.Arguments = "/C " + commandText;
			process.StartInfo = startInfo;
			process.Start();
			Parser.ParsingContext.Parameters.Add("ExitCode", Parameter.NewParameter(process.ExitCode));
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
