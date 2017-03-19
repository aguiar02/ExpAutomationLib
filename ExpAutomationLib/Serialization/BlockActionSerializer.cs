using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Serialization
{
	public class BlockActionSerializer : ISerializer
	{
		public object Deserialize(System.Xml.XmlReader xmlReader)
		{
			BlockAction blockAction = new BlockAction();
			blockAction.ActionList = new List<IAction>();
			xmlReader.ReadStartElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			while (xmlReader.IsStartElement("Action"))
			{
				IAction action = SerializerHelper.DeserializeAction(xmlReader);
				blockAction.ActionList.Add(action);
			}
			xmlReader.ReadEndElement();
			SerializerHelper.ReadWhiteSpace(xmlReader);
			return blockAction;
		}

		public void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{
			BlockAction ba = (BlockAction)o;
			foreach (var action in ba.ActionList)
			{
				ISerializer actionSerializer = SerializerProvider.GetSerializer(action.GetType());
				xmlWriter.WriteStartElement("Action");
				//xmlWriter.WriteAttributeString("AssemblyQualifiedName", action.GetType().AssemblyQualifiedName);
				xmlWriter.WriteAttributeString("Type", SerializerHelper.GetActionName(action.GetType()));
				actionSerializer.Serialize(xmlWriter, action);
				xmlWriter.WriteEndElement();
			}
		}
	}
}
