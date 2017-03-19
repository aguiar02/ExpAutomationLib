using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Action
{
	public interface IHasInnerAction : IAction
	{
		IAction InnerAction { get; set; }
	}
}
