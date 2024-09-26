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
	public class ErrorLoggerTests
	{
		[Test]
		public void Log_WhenCalled_SetTheLastErrorProperty()
		{
			var logger = new ErrorLogger();

			logger.Log("abc");

			Assert.That(logger.LastError, Is.EqualTo("abc"));
		}

		[TestCase(null)]
		[TestCase("")]
		[TestCase(" ")]
		public void Log_WhenCalled_ThrowArgumentNullException(string error)
		{
			var logger = new ErrorLogger();

			Assert.Throws<ArgumentNullException>(() => logger.Log(error));
			// OR
			//Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
		}

		[Test]
		public void Log_ValidError_RaiseErrorLoggedEvent()
		{
			var logger = new ErrorLogger();

			// subscribe to the event to get the GUID
			// sender is the source of the event
			// args is the event arguments
			var id = Guid.Empty;
			logger.ErrorLogged += (sender, args) => { id = args; };

			logger.Log("a");

			Assert.That(id, Is.Not.EqualTo(Guid.Empty));
		}
	}
}
