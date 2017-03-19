using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using ExpAutomationLib.Trigger;

namespace ExpAutomationLib.Serialization
{
	public class TriggerSerializer : ISerializer
	{
		private Type type;
		public TriggerSerializer(Type type)
		{
			this.type = type;
		}

		public object Deserialize(System.Xml.XmlReader xmlReader)
		{
			if (!xmlReader.IsStartElement("Trigger"))
				throw new System.Exception("Element Trigger expected");

			//xmlReader.MoveToAttribute("AssemblyQualifiedName");
			xmlReader.MoveToAttribute("Type");
			Type type = SerializerHelper.TriggerTypes[xmlReader.Value];//Type.GetType(xmlReader.Value);
			if (type == null)
				throw new System.Exception("Tipo não identificado.");

			ITrigger trigger = (ITrigger)Activator.CreateInstance(type);
			xmlReader.ReadStartElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			while (xmlReader.IsStartElement())
			{
				SerializerHelper.FillProperty(xmlReader, trigger);
				SerializerHelper.ReadWhiteSpace(xmlReader);
			}

			xmlReader.ReadEndElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			return trigger;
		}

		public void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{
			foreach (var prop in o.GetType().GetProperties().Where(p => p.Name != "Parser" && p.Name != "IsStateActive" && p.Name != "FormHandle"))
			{
				xmlWriter.WriteElementString(prop.Name, prop.GetValue(o, null).ToString());
			}
		}
	}
}
