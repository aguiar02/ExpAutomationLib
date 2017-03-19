using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using ExpCalculatorLib;
using System.Threading;

namespace ExpAutomationLib.Trigger
{
	public class DrivePlugTrigger : IWindowsMessageTrigger
	{
		const int WM_DEVICECHANGE = 0x0219; //see msdn site
		const int DBT_DEVICEARRIVAL = 0x8000;
		const int DBT_DEVICEREMOVALCOMPLETE = 0x8004;
		const int DBT_DEVTYPVOLUME = 0x00000002;  

		[StructLayout(LayoutKind.Sequential)] //Same layout in mem
		public struct DEV_BROADCAST_VOLUME
		{
			public int dbcv_size;
			public int dbcv_devicetype;
			public int dbcv_reserved;
			public int dbcv_unitmask;
		}

		private static char DriveMaskToLetter(int mask)
		{
			char letter;
			string drives = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //1 = A, 2 = B, 3 = C
			int cnt = 0;
			int pom = mask / 2;
			while (pom != 0)    // while there is any bit set in the mask shift it right        
			{
				pom = pom / 2;
				cnt++;
			}
			if (cnt < drives.Length)
				letter = drives[cnt];
			else
				letter = '?';
			return letter;
		}

		public event TriggerEvent OnTriggerEvent;
		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			Parser.ParsingContext.Parameters.Add("DrivePlugLetter", Parameter.NewParameter(typeof(string)));
		}

		public string VolumeLabelExpression { get; set; }
		private string volumeLabel;

		public void Setup()
		{
			volumeLabel = ExpHelper.EvalToString(Parser, VolumeLabelExpression);
		}

		public void ShutDown()
		{
		}

		public void WindowsMessageReceived(ref Message m)
		{
			if (m.Msg == WM_DEVICECHANGE)
			{
				DEV_BROADCAST_VOLUME vol = (DEV_BROADCAST_VOLUME)Marshal.PtrToStructure(m.LParam, typeof(DEV_BROADCAST_VOLUME));
				if ((m.WParam.ToInt32() == DBT_DEVICEARRIVAL) && (vol.dbcv_devicetype == DBT_DEVTYPVOLUME))
				{
					var letter = DriveMaskToLetter(vol.dbcv_unitmask).ToString();
					if (DriveInfo.GetDrives()
						.Any(d => d.DriveType == DriveType.Removable 
							&& d.Name[0] == letter[0]
							&& (this.volumeLabel == "" || d.VolumeLabel == this.volumeLabel)
						))
					{
						Parser.ParsingContext.Parameters["DrivePlugLetter"].ParameterValue = letter;
						OnTriggerEvent.Invoke();
					}
				}
				if ((m.WParam.ToInt32() == DBT_DEVICEREMOVALCOMPLETE) && (vol.dbcv_devicetype == DBT_DEVTYPVOLUME))
				{
					//MessageBox.Show("usb out");
				}
			}
		}

		public IntPtr FormHandle { get; set; }
	}
}
