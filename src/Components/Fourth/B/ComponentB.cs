using Components.Fifth;
using Components.Messages.Third;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Fourth.B
{
	public class ComponentB : BaseComponent<MessageB, Message5>
	{

		public ComponentB(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message5 Process(MessageB message)
		{
			return new Message5 { Message = $"ComponentB: \n{message.ToString()}" };
		}
	}
}
