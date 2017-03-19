using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using ExpAutomationLib.Action;
using ExpAutomationLib.Trigger;
using System.IO;

namespace ExpAutomationLib.Serialization
{
	public class SerializerHelper
	{
		public static Dictionary<string, Type> ActionTypes;
		public static Dictionary<string, Type> TriggerTypes;

		static SerializerHelper()
		{
			ActionTypes = new Dictionary<string, Type>();
			TriggerTypes = new Dictionary<string, Type>();

			LoadTypesFromAssembly(Assembly.GetExecutingAssembly());
			foreach (var fileName in Directory.GetFiles("./", "*.dll"))
			{
				Assembly asm = Assembly.LoadFrom(fileName);
				if (asm.Equals(Assembly.GetExecutingAssembly()))
					continue;

				LoadTypesFromAssembly(asm);

			}
		}

		public static void FillProperty(XmlReader xmlReader, object targetObj)
		{
			if (targetObj == null)
				throw new NullReferenceException();

			Type type = targetObj.GetType();
			PropertyInfo prop = type.GetProperty(xmlReader.Name);
			if (prop == null)
				throw new System.Exception();

			if (prop.PropertyType == typeof(bool))
				prop.SetValue(targetObj, xmlReader.ReadElementContentAsString().Equals("True"), null);
			else if (prop.PropertyType == typeof(DateTime))
				prop.SetValue(targetObj, DateTime.Parse(xmlReader.ReadElementContentAsString()), null);
			else
				prop.SetValue(targetObj, xmlReader.ReadElementContentAs(prop.PropertyType, null), null);
		}

		public static void ReadWhiteSpace(XmlReader xmlReader)
		{
			if (xmlReader.NodeType == XmlNodeType.Whitespace)
				xmlReader.Read();
		}

		public static IAction DeserializeAction(XmlReader xmlReader)
		{
			if (!xmlReader.IsStartElement("Action"))
				throw new System.Exception("Element Action expected");

			xmlReader.MoveToAttribute("Type");
			Type type = SerializerHelper.ActionTypes[xmlReader.Value];
			if (type == null)
				throw new System.Exception("Tipo não identificado.");

			ISerializer ser = SerializerProvider.GetSerializer(type);
			IAction action = (IAction)ser.Deserialize(xmlReader);
			return action;
		}

		private static void LoadTypesFromAssembly(Assembly asm)
		{
			Type[] types = asm.GetTypes();
			foreach (var type in types
				.Where(t => !t.IsInterface
					&& !t.IsAbstract
					&& typeof(IAction).IsAssignableFrom(t)))
			{
				ActionTypes.Add(GetActionName(type), type);
			}

			foreach (var type in types
				.Where(t => !t.IsInterface
					&& !t.IsAbstract
					&& typeof(ITrigger).IsAssignableFrom(t)))
			{
				TriggerTypes.Add(GetTriggerName(type), type);
			}
		}

		public static string GetActionName(Type type)
		{
			int idx = type.Name.IndexOf("Action");
			if (idx == -1)
				return null;
			return type.Name.Substring(0, idx);
		}

		public static string GetTriggerName(Type type)
		{
			int idx = type.Name.IndexOf("Trigger");
			if (idx == -1)
				return null;
			return type.Name.Substring(0, idx);
		}
	}
}
