using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace ExpAutomationLib.Serialization
{
	public interface ISerializer
	{
		object Deserialize(XmlReader xmlReader);
		void Serialize(XmlWriter xmlWriter, object o);
	}
}
