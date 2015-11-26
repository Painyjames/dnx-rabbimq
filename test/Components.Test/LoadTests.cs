using Autofac;
using Components.Messages.First;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Components.Test
{
	public class LoadTests
    {

		private IContainer _container;

		[Fact]
        public void Component1MessageLoadTest()
        {
			//Arrange
			var module = new CoreModule();
			_container = module.Start<LoadTests>();
			var message1 = new Message1
			{
				Message = "test message"
			};
			var numberOfMessages = 100000;

			//Act
			var producer = _container.Resolve<IProducer>();

			//Assert
			for (var i = 0; i < numberOfMessages; i++)
			{
				producer.Produce(message1);
			}
		}
    }
}
