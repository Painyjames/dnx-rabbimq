using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

namespace Core
{
	public abstract class BaseComponent<TIn, TOut> : IConsumer<TIn>, IProducer<TOut>, IStartable where TIn: BaseMessage where TOut : BaseMessage
	{
		private readonly IBasicConsumer _basicConsumer;
		private readonly IModel _channel;
		private readonly IUtilities _utilities;
		private readonly IProducer<TOut> _producer;
		private string _consumerTag;

		public BaseComponent(IBasicConsumer basicConsumer,
							 IConnectionFactory connectionFactory,
							 IUtilities utilities,
							 IProducer<TOut> producer) {
			_basicConsumer = basicConsumer;
			_utilities = utilities;
            var connection = connectionFactory.CreateConnection();
			_channel = connection.CreateModel();
			CreateBindings();
			_producer = producer;
        }

		public void CreateBindings()
		{
			_channel.ExchangeDeclare(_utilities.ExchangeName<TIn>(), ExchangeType.Direct);
			_channel.QueueDeclare(_utilities.QueueName<TIn>(), true, false, false, null);
			//TODO: bind to routing keys instead of wildcard
			_channel.QueueBind(_utilities.QueueName<TIn>(), _utilities.ExchangeName<TIn>(), _utilities.RoutingKey<TIn>(), null);
		}

		public event EventHandler<ConsumerEventArgs> ConsumerCancelled;

		public void Consume(TIn message)
		{
			try
			{

				var messageProcessed = Process(message);

				if (typeof(TIn) == typeof(TOut)) //TODO: change this?
					return;

				Produce(messageProcessed);
			}
			catch (Exception ex)
			{
			}
		}

		public void Produce(TOut message)
		{
			_producer.Produce(message);
        }

		public void Start()
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += (ch, ea) =>
			{
				Consume(JsonConvert.DeserializeObject<TIn>(Encoding.UTF8.GetString(ea.Body)));
				_channel.BasicAck(ea.DeliveryTag, false);
			};
			_consumerTag = _channel.BasicConsume(_utilities.QueueName<TIn>(), false, consumer);
		}

		public abstract TOut Process(TIn message);
		
	}

	public static class BaseComponentExtensions
	{
		public static bool IsBaseComponent(this Type type)
		{
			var baseType = type.GetTypeInfo().BaseType;
			return baseType?.IsGenericType == true && baseType.GetGenericTypeDefinition() == typeof(BaseComponent<,>);
		}
	}

}
