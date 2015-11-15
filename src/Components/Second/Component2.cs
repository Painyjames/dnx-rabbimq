using Components.Messages;
using Components.Messages.Second;
using Components.Messages.Third;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Second
{
	public class Component2 : BaseComponent<Message2, Message3>
	{

		public Component2(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer<Message3> producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message3 Process(Message2 message)
		{
			return new Message3
			{
				Message = message.Message,
				Number = message.Number,
				IsTrue = message.Message.Length > message.Number
			};
		}
	}
}
