using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExpAutomationLib.Trigger
{
	public interface IWindowsMessageTrigger : IEventTrigger
	{
		void WindowsMessageReceived(ref Message m);
		IntPtr FormHandle { get; set; }
	}
}
