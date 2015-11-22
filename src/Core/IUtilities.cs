using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public interface IUtilities
    {
		string ExchangeName<T>();
		string QueueName<T>();
		string RoutingKey<T>();

		string ExchangeName(Type type);
		string QueueName(Type type);
		string RoutingKey(Type type);
	}
}
