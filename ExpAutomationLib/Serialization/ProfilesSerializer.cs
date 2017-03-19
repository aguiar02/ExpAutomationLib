using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Trigger;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Serialization
{
	public class ProfilesSerializer : ISerializer
	{
		public object Deserialize(System.Xml.XmlReader xmlReader)
		{
			List<Profile> profiles = new List<Profile>();

			xmlReader.ReadStartElement("Profiles");
			while (xmlReader.IsStartElement("Profile"))
			{
				Profile item = new Profile();
				xmlReader.ReadStartElement();
				item.Name = xmlReader.ReadElementString("Name");
				item.Enabled = bool.Parse(xmlReader.ReadElementString("Enabled"));

				if (xmlReader.IsStartElement("EventTrigger"))
				{
					xmlReader.ReadStartElement();
					ISerializer ser = SerializerProvider.GetSerializer(typeof(ITrigger));
					IEventTrigger trigger = (IEventTrigger)ser.Deserialize(xmlReader);
					item.EventTrigger = trigger;
					xmlReader.ReadEndElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
				}
				item.StateTriggers = new List<IStateTrigger>();
				if (xmlReader.IsStartElement("StateTriggers"))
				{
					xmlReader.ReadStartElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
					while (xmlReader.IsStartElement())
					{
						ISerializer ser = SerializerProvider.GetSerializer(typeof(ITrigger));
						IStateTrigger trigger = (IStateTrigger)ser.Deserialize(xmlReader);
						item.StateTriggers.Add(trigger);
					}
					xmlReader.ReadEndElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
				}
				if (xmlReader.IsStartElement("EnterStateAction"))
				{
					xmlReader.ReadStartElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
					item.ActionToExecute = SerializerHelper.DeserializeAction(xmlReader);
					xmlReader.ReadEndElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
				}
				if (xmlReader.IsStartElement("ExitStateAction"))
				{
					xmlReader.ReadStartElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
					item.ActionToExecute = SerializerHelper.DeserializeAction(xmlReader);
					xmlReader.ReadEndElement();
					SerializerHelper.ReadWhiteSpace(xmlReader);
				} 
				xmlReader.ReadEndElement();
				SerializerHelper.ReadWhiteSpace(xmlReader);
				profiles.Add(item);
			}
			xmlReader.ReadEndElement();
			return profiles;
		}

		public void Serialize(System.Xml.XmlWriter xmlWriter, object o)
		{

			List<Profile> items = (List<Profile>)o;

			xmlWriter.WriteStartElement("Profiles");
			foreach (var profile in items)
			{
				xmlWriter.WriteStartElement("Profile");
				xmlWriter.WriteElementString("Name", profile.Name);
				xmlWriter.WriteElementString("Enabled", profile.Enabled.ToString());
				if (profile.EventTrigger != null)
				{
					ISerializer eventTriggerSerializer = SerializerProvider.GetSerializer(typeof(IEventTrigger));
					xmlWriter.WriteStartElement("EventTrigger");
					xmlWriter.WriteStartElement("Trigger");
					//xmlWriter.WriteAttributeString("AssemblyQualifiedName", profile.EventTrigger.GetType().AssemblyQualifiedName);
					xmlWriter.WriteAttributeString("Type", SerializerHelper.GetTriggerName(profile.EventTrigger.GetType()));
					eventTriggerSerializer.Serialize(xmlWriter, profile.EventTrigger);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}
				
				//StateTriggers
				xmlWriter.WriteStartElement("StateTriggers");
				ISerializer stateTriggersSerializer = SerializerProvider.GetSerializer(typeof(IStateTrigger));
				foreach (var stateTrigger in profile.StateTriggers)
				{
					xmlWriter.WriteStartElement("Trigger");
					//xmlWriter.WriteAttributeString("AssemblyQualifiedName", stateTrigger.GetType().AssemblyQualifiedName);
					xmlWriter.WriteAttributeString("Type", SerializerHelper.GetTriggerName(stateTrigger.GetType()));
					stateTriggersSerializer.Serialize(xmlWriter, stateTrigger);
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteFullEndElement();

				//EnterStateAction
				ISerializer actionToExecuteSerializer = SerializerProvider.GetSerializer(profile.ActionToExecute.GetType());
				xmlWriter.WriteStartElement("EnterStateAction");
				xmlWriter.WriteStartElement("Action");
				//xmlWriter.WriteAttributeString("AssemblyQualifiedName", profile.ActionToExecute.GetType().AssemblyQualifiedName);
				xmlWriter.WriteAttributeString("Type", SerializerHelper.GetActionName(profile.ActionToExecute.GetType()));
				actionToExecuteSerializer.Serialize(xmlWriter, profile.ActionToExecute);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();

				//ExitStateAction
				ISerializer exitStateActionSerializer = SerializerProvider.GetSerializer(profile.ActionToExecute.GetType());
				if (profile.ActionOnExitState != null)
				{
					xmlWriter.WriteStartElement("ExitStateAction");
					xmlWriter.WriteStartElement("Action");
					//xmlWriter.WriteAttributeString("AssemblyQualifiedName", profile.ActionOnExitState.GetType().AssemblyQualifiedName);
					xmlWriter.WriteAttributeString("Type", SerializerHelper.GetActionName(profile.ActionOnExitState.GetType()));
					exitStateActionSerializer.Serialize(xmlWriter, profile.ActionOnExitState);
					xmlWriter.WriteEndElement();
					xmlWriter.WriteEndElement();
				}

				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
		}
	}
}
