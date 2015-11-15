using Microsoft.Extensions.OptionsModel;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
	public class ConnectionFactory : RabbitMQ.Client.ConnectionFactory, IConnectionFactory
	{
		private static readonly Dictionary<string, IConnection> _connections = new Dictionary<string, IConnection>();

		private static readonly object _connectionLock = new object();


		public ConnectionFactory(IOptions<ConnectionSettings> connectionSettingsOptions)
		{
			var connectionSettings = connectionSettingsOptions.Value;
            RequestedHeartbeat = connectionSettings.RequestedHeartbeat;
			HostName = connectionSettings.HostName;
			UserName = connectionSettings.UserName;
			Password = connectionSettings.Password;
			VirtualHost = connectionSettings.VirtualHost;
        }
		
		public new IConnection CreateConnection()
		{
			IConnection connection;
			lock (_connectionLock)
			{
				RabbitMQ.Client.ConnectionFactory connectionFactory = this;

				if (_connections.ContainsKey($"{VirtualHost}_{HostName}"))
                {
					return _connections[$"{VirtualHost}_{HostName}"];
				}
				else
				{
					connection = connectionFactory.CreateConnection();
					_connections.Add($"{VirtualHost}_{HostName}", connection);
				}

				return connection;
			}
		}
		
		public void ResetConnections()
		{
			lock (_connectionLock)
			{
				_connections.Clear();
			}
		}
		
	}
}

