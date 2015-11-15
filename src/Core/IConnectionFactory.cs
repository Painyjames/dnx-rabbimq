using RabbitMQ.Client;

namespace Core
{
    public interface IConnectionFactory
	{
		IConnection CreateConnection();
		
		void ResetConnections();
	}
}