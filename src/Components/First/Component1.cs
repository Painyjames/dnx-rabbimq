using Components.Messages;
using Components.Messages.First;
using Components.Messages.Second;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.First
{
	public class Component1 : BaseComponent<Message1, Message2>
	{
		private readonly Random _random;

		public Component1(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
			_random = new Random();
		}

		public override Message2 Process(Message1 message)
		{
			return new Message2
			{
				Message = message.Message,
				Number = _random.Next()*message.Message.Length*2
			};
		}
	}
}
