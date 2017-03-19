using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpAutomationLib.Trigger;
using ExpAutomationLib.Action;

namespace ExpAutomationLib.Serialization
{
	public class SerializerProvider
	{
		protected static Dictionary<Type, ISerializer> serializers;

		static SerializerProvider()
		{
			serializers = new Dictionary<Type, ISerializer>();
			serializers.Add(typeof(BlockAction), new BlockActionSerializer());
		}

		public static ISerializer GetSerializer(Type type)
		{
			if (serializers.ContainsKey(type))
				return serializers[type];

			if (typeof(ITrigger).IsAssignableFrom(type))
				return new TriggerSerializer(type);

			if (typeof(IHasInnerAction).IsAssignableFrom(type))
				return new HasInnerActionSerializer(type);

			if (typeof(IAction).IsAssignableFrom(type))
				return new AtomicActionSerializer(type);
			
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
			{
				Type itemType = type.GetGenericArguments()[0];
				Type listSerializerType = typeof(ListSerializer<>).MakeGenericType(itemType);
				return (ISerializer)Activator.CreateInstance(listSerializerType);
			}

			return null;
		}
	}
}
