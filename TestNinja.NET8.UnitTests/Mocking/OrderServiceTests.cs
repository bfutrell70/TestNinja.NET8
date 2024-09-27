using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Mocking;

namespace TestNinja.NET8.UnitTests.Mocking
{
	[TestFixture]
	public class OrderServiceTests
	{
		[Test]
		public void PlaceOrder_WhenCalled_StoresTheOrder()
		{
			// arrange
			var storage = new Mock<IStorage>();
			var service = new OrderService(storage.Object);
			var order = new Order();
			
			// act
			service.PlaceOrder(order);

			// assert
			storage.Verify(s => s.Store(order));
		}
	}
}
