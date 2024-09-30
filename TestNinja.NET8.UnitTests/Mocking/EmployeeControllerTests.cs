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
	public class EmployeeControllerTests
	{
		private Mock<IEmployeeRepostory> _employeeRepository = default!;
		private EmployeeController _sut = default!;

		[SetUp]
		public void SetUp()
		{
			_employeeRepository = new Mock<IEmployeeRepostory>();
			_sut = new EmployeeController(_employeeRepository.Object);
		}

		[Test]
		public void DeleteEmployee_WhenCalled_ReturnsCorrectObject()
		{
			// arrange

			// act
			var result = _sut.DeleteEmployee(1);

			// assert
			Assert.That(result, Is.TypeOf<RedirectResult>());
		}

		[Test]
		public void DeleteEmployee_WhenCalled_DeleteTheEmployeFromDb()
		{
			// arrange

			// act
			_sut.DeleteEmployee(1);

			// assert
			_employeeRepository.Verify(x => x.DeleteEmployee(1), Times.Once);
		}
	}
}
