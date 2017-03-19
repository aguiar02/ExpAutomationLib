using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;

namespace ExpAutomationLib.Action
{
	public class RunAplicationAction : IAction
	{
		public string ExecutablePathExpression { get; set; }
		private string executablePath;
		public string ArgumentsExpression { get; set; }
		private string arguments;
		public bool MinimizeWindow { get; set; }

		public void ExecuteAction()
		{
			executablePath = ExpHelper.EvalToString(Parser, ExecutablePathExpression);
			arguments = ExpHelper.EvalToString(Parser, ArgumentsExpression);
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
			if (MinimizeWindow)
				startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;

			startInfo.FileName = executablePath;
			startInfo.Arguments = arguments;
			process.StartInfo = startInfo;
			process.Start();
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
