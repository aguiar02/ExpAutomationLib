using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Serialization
{
	public class HasInnerActionSerializer : AtomicActionSerializer
	{
		public HasInnerActionSerializer(Type type)
			: base (type)
		{
		}

		public override object Deserialize(System.Xml.XmlReader xmlReader)
		{
			return base.Deserialize(xmlReader);
		}

		public override void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{
			base.Serialize(xmlWriter, o);
			IHasInnerAction action = (IHasInnerAction)o;
			xmlWriter.WriteStartElement("InnerAction");
			xmlWriter.WriteStartElement("Action");
			//xmlWriter.WriteAttributeString("AssemblyQualifiedName", action.InnerAction.GetType().AssemblyQualifiedName);
			xmlWriter.WriteAttributeString("Type", SerializerHelper.GetActionName(action.InnerAction.GetType()));
			ISerializer actionSerializer = SerializerProvider.GetSerializer(action.GetType());
			actionSerializer.Serialize(xmlWriter, action.InnerAction);
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
		}
	}
}
