using Components.Fourth.A;
using Components.Fourth.B;
using Components.Fourth.C;
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
	public class Component3 : BaseComponent<Message3, IList<BaseMessage>>
	{
		
		public Component3(IBasicConsumer basicConsumer,
							 Core.IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer producer) : base(basicConsumer, connectionFactory, utilities, producer)
		{
		}

		public override IList<BaseMessage> Process(Message3 message)
		{
			var producedMessages = new List<BaseMessage>
			{
				new MessageA { Message = $"this is message to A \n{message.ToString()}" },
                new MessageB { Message = $"this is message to B \n{message.ToString()}" },
				new MessageC { Message = $"this is message to C \n{message.ToString()}" }
			};
			return producedMessages;
		}
	}
}
