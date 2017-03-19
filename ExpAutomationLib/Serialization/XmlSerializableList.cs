using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace ExpAutomationLib.Serialization
{
	public class XmlSerializableList<T> : List<T>, IXmlSerializable
	{
		private string elementCollectionName;
		private string elementItemName;

		public XmlSerializableList()
			:base()
		{
			this.elementCollectionName = "Item";
		}

		public XmlSerializableList(string elementCollectionName)
			: base()
		{
			this.elementCollectionName = elementCollectionName;
		}

		public XmlSerializableList(string elementCollectionName, string elementItemName)
			: base()
		{
			this.elementCollectionName = elementCollectionName;
			this.elementItemName = elementItemName;
		}

		public System.Xml.Schema.XmlSchema GetSchema() { return null; }

		public void ReadXml(XmlReader reader)
		{
			reader.ReadStartElement(elementCollectionName);
			while (reader.IsStartElement("Item"))
			{
				Type type = Type.GetType(reader.GetAttribute("AssemblyQualifiedName"));
				XmlSerializer serial = new XmlSerializer(type);

				reader.ReadStartElement("Item");
				this.Add((T)serial.Deserialize(reader));
				reader.ReadEndElement();
			}
			reader.ReadEndElement();
		}

		public void WriteXml(XmlWriter writer)
		{
			foreach (T item in this)
			{
				writer.WriteStartElement("Item");
				writer.WriteAttributeString("AssemblyQualifiedName", item.GetType().AssemblyQualifiedName);
				XmlSerializer xmlSerializer = new XmlSerializer(item.GetType());
				xmlSerializer.Serialize(writer, item);
				writer.WriteEndElement();
			}
		}
	}
}
