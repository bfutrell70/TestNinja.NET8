using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Mocking;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests.Mocking
{
	[TestFixture]
	public class ProductTests
	{
		[Test]
		public void GetPrice_GoldCustomer_Apply30PercentDiscount()
		{
			// arrange
			var product = new Product { ListPrice = 100 };

			// act
			var result = product.GetPrice(new Customer {  IsGold = true });

			// assert
			Assert.That(result, Is.EqualTo(70));
		}

		[Test]
		public void GetPrice_GoldCustomer_Apply30PercentDiscount2()
		{
			// example of Mock Abuse :)

			// arrange
			var customer = new Mock<ICustomer>();
			customer.Setup(c => c.IsGold).Returns(true);
			var product = new Product { ListPrice = 100 };

			// act
			var result = product.GetPrice(customer.Object);

			// assert
			Assert.That(result, Is.EqualTo(70));
		}
	}
}
