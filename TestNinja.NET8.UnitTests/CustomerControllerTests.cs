using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Fundamentals;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests
{
	[TestFixture]
	public class CustomerControllerTests
	{
		[Test]
		public void GetCustomer_IdIsZero_ReturnNotFound()
		{
			var controller = new CustomerController();

			var result = controller.GetCustomer(0);

			// object is the type specified
			// NotFound
			Assert.That(result, Is.TypeOf<NotFound>());
						
			// object is the type specified or one of its derivatives
			// NotFound or one of its derivatives
			//Assert.That(result, Is.InstanceOf<NotFound>());
		}

		[Test]
		public void GetCustomer_IsIsNotZero_ReturnOk()
		{

		}
	}
}
