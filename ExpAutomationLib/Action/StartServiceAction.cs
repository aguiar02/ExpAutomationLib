using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace ExpAutomationLib.Action
{
	//http://www.csharp-examples.net/restart-windows-service/

	public class StartServiceAction : IAction
	{
		public string ServiceName { get; set; }

		public static void StartService(string serviceName)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				//TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

				service.Start();
				service.WaitForStatus(ServiceControllerStatus.Running);
			}
			catch
			{
				
			}
		}

		public void ExecuteAction()
		{
			StartService(ServiceName);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
