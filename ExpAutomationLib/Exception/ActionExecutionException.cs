using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpAutomationLib.Exception
{
	[Serializable]
	public class ActionExecutionException : System.Exception
	{
		public ActionExecutionException() { }
		public ActionExecutionException(string message) : base(message) { }
		public ActionExecutionException(string message, System.Exception inner) : base(message, inner) { }
		protected ActionExecutionException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context)
			: base(info, context) { }
	}
}
