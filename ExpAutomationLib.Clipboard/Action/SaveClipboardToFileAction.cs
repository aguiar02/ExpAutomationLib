using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ExpAutomationLib.Action
{
	public class SaveClipboardToFileAction : IAction
	{
		public string FilePathExpression { get; set; }
		private string filePath;

		public bool Sobreescrever { get; set; }
		public bool AppendFile { get; set; }

		public void ExecuteAction()
		{
			filePath = ExpHelper.EvalToString(Parser, FilePathExpression);

			if (File.Exists(filePath) && !Sobreescrever && AppendFile)
			{
				StreamWriter fileStream = File.AppendText(filePath);
				fileStream.Write(System.Windows.Forms.Clipboard.GetText());
				fileStream.Close();
			}
			else if (!File.Exists(filePath) || Sobreescrever)
				File.WriteAllText(filePath, System.Windows.Forms.Clipboard.GetText());

		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
