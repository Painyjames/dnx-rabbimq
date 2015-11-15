using Components.Messages;
using Components.Messages.Third;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Third
{
	public class Component3 : BaseComponent<Message3, Message3>
	{

		public Component3(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer<Message3> producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message3 Process(Message3 message)
		{
			Console.WriteLine($"Finished processing\n{message.ToString()}");
			return message;
		}
	}
}
