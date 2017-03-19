using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Trigger;
using ExpAutomationLib.Action;
using ExpAutomationLib.Serialization;

namespace ExpAutomationLib
{
	public class Profile
	{
		public string Name { get; set; }
		public bool Enabled { get; set; }
		
		public List<IStateTrigger> StateTriggers { get; set; }
		public IEventTrigger EventTrigger { get; set; }
		public IAction ActionToExecute { get; set; }
		public IAction ActionOnExitState { get; set; }
		public bool IsAllStateActive { get; set; }
	}
}
