using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib;
using System.IO;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	public class DrivePresentTrigger : IStateTrigger
	{
		private bool isStateActive;
		public bool IsStateActive
		{
			get { return isStateActive; }
		}

		Thread triggerThread;

		public string VolumeLabelExpression { get; set; }
		private string volumeLabel;

		public string DriveLetter { get; set; }

		public void ExecuteMonitor()
		{
			isStateActive = false;
			volumeLabel = ExpHelper.EvalToString(Parser, VolumeLabelExpression);
			triggerThread = new Thread(() =>
			{
				while (true)
				{
					isStateActive = DriveInfo.GetDrives().Any(d => 
						   (!string.IsNullOrEmpty(DriveLetter) || d.Name == DriveLetter)
						&& (!string.IsNullOrEmpty(volumeLabel) || d.VolumeLabel == volumeLabel));
					Thread.Sleep(500);
				}
			});
			triggerThread.Start();
		}

		public ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			
		}

		public void ShutDown()
		{
			triggerThread.Abort();
		}
	}
}
