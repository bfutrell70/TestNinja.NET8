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
	public class VideoServiceTests
	{
		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnError()
		{
			// arrange
			var service = new VideoService();

			// act
			var result = service.ReadVideoTitle(new FakeFileReader());

			// assert
			Assert.That(result, Does.Contain("error").IgnoreCase);
		}
	}
}
