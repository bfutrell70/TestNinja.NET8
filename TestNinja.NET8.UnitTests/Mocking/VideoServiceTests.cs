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
	public class VideoServiceTests
	{
		private Mock<IFileReader> _fileReader = default!;
		private VideoService _videoService = default!;

		[SetUp]
		public void SetUp()
		{
			_fileReader = new Mock<IFileReader>();
			_videoService = new VideoService(_fileReader.Object);
		}

		[Test]
		public void ReadVideoTitle_EmptyFile_ReturnError()
		{
			// arrange
			_fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

			// act
			var result = _videoService.ReadVideoTitle();

			// assert
			Assert.That(result, Does.Contain("error").IgnoreCase);
		}
	}
}
