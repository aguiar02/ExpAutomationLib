using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ExpAutomationLib.Serialization
{
	public class ListSerializer<T> : ISerializer
	{
		public object Deserialize(System.Xml.XmlReader xmlReader)
		{
			List<T> lista = new List<T>();
			while (xmlReader.IsStartElement("Item"))
			{
				Type type = Type.GetType(xmlReader.GetAttribute("AssemblyQualifiedName"));
				ISerializer serial = SerializerProvider.GetSerializer(type);
				xmlReader.ReadStartElement("Item");
				lista.Add((T)serial.Deserialize(xmlReader));
				xmlReader.ReadEndElement();
			}
			return lista;
		}

		public void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{
			IList list = (IList)o;
			foreach (object item in list)
			{
				xmlWriter.WriteStartElement("Item");
				xmlWriter.WriteAttributeString("AssemblyQualifiedName", item.GetType().AssemblyQualifiedName);
				ISerializer serializer = SerializerProvider.GetSerializer(item.GetType());
				serializer.Serialize(xmlWriter, item);
				xmlWriter.WriteEndElement();
			}
		}
	}
}
