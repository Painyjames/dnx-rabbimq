using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class Utilities : IUtilities
    {
		public string ExchangeName<T>()
		{
			return typeof(T).Namespace;
		}
		public string QueueName<T>()
		{
			return $"{ExchangeName<T>()}.{typeof(T).Name}";
		}
		public string RoutingKey<T>()
		{
			return $"{QueueName<T>()}.*";
		}

		public string ExchangeName(Type type)
		{
			return type.Namespace;
		}
		public string QueueName(Type type)
		{
			return $"{ExchangeName(type)}.{type.Name}";
		}
		public string RoutingKey(Type type)
		{
			return $"{QueueName(type)}.*";
		}
	}
}
