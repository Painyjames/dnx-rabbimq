using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Newtonsoft.Json;

namespace Core
{
	public class Producer<T> : IProducer<T>
	{
		private readonly IModel _channel;
		private readonly IUtilities _utilities;

		public Producer(IConnectionFactory connectionFactory,
							 IUtilities utilities)
		{
			_utilities = utilities;
			var connection = connectionFactory.CreateConnection();
			_channel = connection.CreateModel();
		}

		public void Produce(T message)
		{
			var payload = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
			var basicProperties = _channel.CreateBasicProperties();
			basicProperties.ContentType = "application/json";
			basicProperties.DeliveryMode = 2; //persistent
			_channel.BasicPublish(_utilities.ExchangeName<T>(),
							  _utilities.RoutingKey<T>(), basicProperties,
							  payload);
		}
	}
}
