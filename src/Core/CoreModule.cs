using Autofac;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.OptionsModel;
using RabbitMQ.Client;
using System.Reflection;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Core
{

	public class Options<T> : IOptions<T> where T : class, new()
	{
		public T Value { get; set; }
	}

	public class CoreModule : Autofac.Module
	{
		private Assembly _assembly;
		private IConfigurationRoot _config;

		public IContainer Start<T>(IList<string> components = null, string environment = "Development", ContainerBuilder containerBuilder = null)
		{
			_assembly = Assembly.GetAssembly(typeof(T));
			_config = Configure(environment);
			var builder = containerBuilder ?? new ContainerBuilder();
			Load(builder);
			LoadComponents(components, builder);
			var container = builder.Build();
			var options = container.Resolve<IOptions<ConnectionSettings>>();
			return container;
        }

		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<QueueingBasicConsumer>()
				.AsImplementedInterfaces().SingleInstance();

			builder.RegisterType<Utilities>()
				.AsImplementedInterfaces().SingleInstance();

			builder.RegisterType<ConnectionFactory>()
				.AsImplementedInterfaces().SingleInstance();

			builder.RegisterType(typeof(Producer))
				.As(typeof(IProducer)).SingleInstance();

			builder.Register(p => _config)
			.As<IConfigurationRoot>().SingleInstance();

			builder.Register(p =>
		   {
			   var config = p.Resolve<IConfigurationRoot>();
			   var connectionSettings = new ConnectionSettings
			   {
				   HostName = config["HostName"],
				   Password = config["Password"],
				   RequestedHeartbeat = Convert.ToUInt16(config["RequestedHeartbeat"]),
				   UserName = config["UserName"],
				   VirtualHost = config["VirtualHost"]
			   };
			   return new Options<ConnectionSettings> { Value = connectionSettings };
		   })
			.As<IOptions<ConnectionSettings>>().SingleInstance();

		}

		private void LoadComponents(IList<string> components, ContainerBuilder builder)
		{
			var types = _assembly.GetReferencedAssemblies().SelectMany(a => Assembly.Load(a)
				.GetTypes()).Where(t => t.IsBaseComponent()).ToList();
			types.AddRange(_assembly.GetTypes().Where(t => t.IsBaseComponent()));

			//Just get the components specified
            if (components?.Any() == true)
				types = types.Where(t => components.Contains(t.Name)).ToList();

			foreach (var componentType in types)
			{
				builder.RegisterType(componentType)
				.As<IStartable>()
				.AutoActivate();
			}
		}

		private IConfigurationRoot Configure(string environment)
		{
			var configBuilder = new ConfigurationBuilder();
			configBuilder
			   .AddEnvironmentVariables();

			var jsonResources = _assembly.GetManifestResourceNames().Where(r => r.EndsWith($"{environment}.json"));
			foreach (var jsonResource in jsonResources)
			{
				var json = new StreamReader(_assembly.GetManifestResourceStream(jsonResource)).ReadToEnd();
				var keyValuePair = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
				configBuilder.AddInMemoryCollection(keyValuePair);
			}

			return configBuilder.Build();
		}

	}
}
