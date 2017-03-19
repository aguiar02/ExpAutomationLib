using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Trigger
{
	public interface IStateTrigger : ITrigger
	{
		bool IsStateActive { get; }
		void ExecuteMonitor();
		void ShutDown();
	}
}
