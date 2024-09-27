using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
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
		private Mock<IVideoRepository> _videoRepository = default!;
		private VideoService _videoService = default!;

		[SetUp]
		public void SetUp()
		{
			_fileReader = new Mock<IFileReader>();
			_videoRepository = new Mock<IVideoRepository>();
			_videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
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

		/*
		 * Write unit tests for GetUnprocessedVideoAsCsv()
		 * - returns a string with video Ids separated by commas
		 * - will need to mock VideoContext, which has a single DbSet for the Video class
		 *		- trying to get this to work has been FUN /s
		 * 
		 * - Video class
		 *		- Id, Title (nullable string in this solution, but was not in the original), IsProcessed
		 *		
		 * Tests:
		 *	if all Video objects are processed [could also be an empty list] (returns an empty string)
		 *	that a comma-separated list of videos Ids are returned of Video objects that aren't processed
		 * 
		 */
		[Test]
		public void GetUnprocessedVideoAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
		{
			// arrange
			// Below line mocks a DbSet. Not how Mosh did the test, but I was rather
			//   proud of it so I'm leaving it here (but commented)
			//_videoContext.SetupGet(x => x.Videos).ReturnsDbSet([]);
			_videoRepository.Setup(x => x.GetUnprocessedVideos()).Returns([]);

			// act
			var result = _videoService.GetUnprocessedVideosAsCsv();

			// assert
			Assert.That(result, Is.EqualTo(""));
		}

		[Test]
		public void GetUnprocessedVideoAsCsv_AFewUnprocessedVideos_AStringWithIdOfUnprocessedVideos()
		{
			// arrange
			_videoRepository.Setup(x => x.GetUnprocessedVideos())
				.Returns(new List<Video>()
				{
					new Video { Id = 1 },
					new Video { Id = 2 },
					new Video { Id = 3 }
				});

			// act
			var result = _videoService.GetUnprocessedVideosAsCsv();

			// assert
			Assert.That(result, Is.EqualTo("1,2,3"));
		}
	}
}
