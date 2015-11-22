using Components.Fifth;
using Components.Messages.Third;
using Core;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Components.Fourth.C
{
	public class ComponentC : BaseComponent<MessageC, Message5>
	{

		public ComponentC(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override Message5 Process(MessageC message)
		{
			return new Message5 { Message = $"ComponentC: \n{message.ToString()}" };
		}
	}
}
