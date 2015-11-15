using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public abstract class BaseMessage
	{
		public string ExchangeName() {
			var namespaceChunks = GetType().Namespace.Split('.');
            return string.Join(".",namespaceChunks.Take(namespaceChunks.Count()-1));
		}
		public string QueueName()
		{
			return GetType().Namespace;
		}
	}
}
