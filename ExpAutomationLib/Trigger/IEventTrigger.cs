using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Trigger
{
	public interface IEventTrigger : ITrigger
	{
		event TriggerEvent OnTriggerEvent;
		void Setup();
		void ShutDown();
	}
}
