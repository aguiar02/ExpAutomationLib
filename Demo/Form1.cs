using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExpAutomationLib;
using ExpAutomationLib.Trigger;
using ExpAutomationLib.Action;
using System.Threading;
using ExpAutomationLib.Clipboard.Trigger;
using System.Xml;
using System.IO;
using ExpAutomationLib.Serialization;

namespace Demo
{
	public partial class Form1 : Form
	{
		AutomationService service = new AutomationService();
		Thread thread;

		public Form1()
		{
			InitializeComponent();
		}




		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			var serializer = new ProfilesSerializer();

			if (!service.IsServiceExecuting)
			{
				service.Profiles = (List<Profile>)serializer.Deserialize(XmlReader.Create(new FileStream("./Profiles.xml", FileMode.Open)));
				service.FormHandle = Handle;
				service.ExecuteService();
			}
			//FileStream fs = new FileStream("./file.xml", FileMode.CreateNew);
			Console.Out.WriteLine("iniciando...");
			//XmlWriter xw = XmlTextWriter.Create(Console.OpenStandardOutput(), new XmlWriterSettings { NewLineChars = "\r\n", Indent = true, Encoding = Encoding.UTF8, });
			StringBuilder sb = new StringBuilder();
			XmlWriter xw = XmlTextWriter.Create(new StringWriter(sb), new XmlWriterSettings { NewLineChars = "\r\n", Indent = true, Encoding = Encoding.UTF8, });
			xw.WriteStartDocument();
			serializer.Serialize(xw, service.Profiles);
			xw.Flush();
			Console.Out.Write(sb.ToString());
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			base.OnClosing(e);
			if (service.IsServiceExecuting) 
				service.ShutDownService();
		}

		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (service.IsServiceExecuting)
				service.WndProc(ref m);
		}
	}
}
