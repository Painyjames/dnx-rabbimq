using Components.Fifth;
using Components.Messages.Third;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Fourth.A
{
	public class ComponentA : BaseComponent<MessageA, Message5>
	{

		public ComponentA(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message5 Process(MessageA message)
		{
			return new Message5 { Message = $"ComponentA: \n{message.ToString()}" };
		}
	}
}
