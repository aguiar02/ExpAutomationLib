using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Serialization
{
	public class AtomicActionSerializer : ISerializer
	{
		private Type type;

		public AtomicActionSerializer(Type type)
		{
			this.type = type;
		}

		public virtual object Deserialize(System.Xml.XmlReader xmlReader)
		{
			IAction action = (IAction)Activator.CreateInstance(type);
			xmlReader.ReadStartElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			while (xmlReader.IsStartElement())
			{
				SerializerHelper.FillProperty(xmlReader, action);
				SerializerHelper.ReadWhiteSpace(xmlReader);
			}
			xmlReader.ReadEndElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			return action;
		}

		public virtual void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{
			foreach (var prop in o.GetType().GetProperties().Where(p => p.Name != "Parser" && p.Name != "InnerAction" && p.Name != "ActionList"))
			{
				xmlWriter.WriteElementString(prop.Name, prop.GetValue(o, null).ToString());
			}
		}
	}
}
