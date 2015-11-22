using Components.Fourth;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Fifth
{
	public class Component5 : BaseComponent<Message5, Message5>
	{

		public Component5(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message5 Process(Message5 message)
		{
			Console.WriteLine($"Finished processing\n{message.Message}");
			return message;
		}
	}
}
