using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Trigger;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExpAutomationLib.Clipboard.Trigger
{
	public class ClipboardUpdateTrigger : IWindowsMessageTrigger
	{
		internal static class NativeMethods
		{
			// See http://msdn.microsoft.com/en-us/library/ms649021%28v=vs.85%29.aspx
			public const int WM_CLIPBOARDUPDATE = 0x031D;
			public static IntPtr HWND_MESSAGE = new IntPtr(-3);

			// See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
			[DllImport("user32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool AddClipboardFormatListener(IntPtr hwnd);

			// See http://msdn.microsoft.com/en-us/library/ms632599%28VS.85%29.aspx#message_only
			[DllImport("user32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

			// See http://msdn.microsoft.com/en-us/library/ms633541%28v=vs.85%29.aspx
			// See http://msdn.microsoft.com/en-us/library/ms649033%28VS.85%29.aspx
			[DllImport("user32.dll", SetLastError = true)]
			public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }

		public void SetParameters()
		{
			
		}

		public IntPtr FormHandle { get; set; }

		public void WindowsMessageReceived(ref Message m)
		{
			if (m.Msg == NativeMethods.WM_CLIPBOARDUPDATE)
			{
				OnTriggerEvent.Invoke();
			}
		}

		public event TriggerEvent OnTriggerEvent;

		public void Setup()
		{
			NativeMethods.SetParent(FormHandle, NativeMethods.HWND_MESSAGE);
			NativeMethods.AddClipboardFormatListener(FormHandle);
		}

		public void ShutDown()
		{
			NativeMethods.RemoveClipboardFormatListener(FormHandle);
		}


	}
}
