using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;

namespace ExpAutomationLib.Action
{
	public class StopServiceAction : IAction
	{
		public string ServiceName { get; set; }

		public static void StopService(string serviceName)
		{
			ServiceController service = new ServiceController(serviceName);
			try
			{
				//TimeSpan timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);

				service.Stop();
				service.WaitForStatus(ServiceControllerStatus.Stopped);
			}
			catch
			{
				// ...
			}
		}

		public void ExecuteAction()
		{
			StopService(ServiceName);
		}

		public ExpCalculatorLib.ExpressionParser Parser { get; set; }
	}
}
