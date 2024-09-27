using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestNinja.NET8.Mocking;
using Assert = NUnit.Framework.Assert;

namespace TestNinja.NET8.UnitTests.Mocking
{
	[TestFixture]
	public class InstallerHelperTests
	{
		// Testing DownloadInstaller(string customerName, string installerName)
		// two outcomes:
		//		no issue accessing the URL, file is downloaded
		//		an issue accessing the URL, false is returned

		private Mock<IFileDownloader> _downloader = default!;
		private InstallerHelper _installerHelper = default!;

		[SetUp]
		public void SetUp() { 
			_downloader = new Mock<IFileDownloader>();
			_installerHelper = new InstallerHelper(_downloader.Object);
		}

		[Test]
		public void DownloadInstaller_DownloadFails_ReturnsFalse()
		{
			// arrange
			// initially I passed in 'a' for the first parameter, but DownloadFile expects a URL
			//_downloader.Setup(d => d.DownloadFile("http://example.com/a/b", "")).Throws<WebException>();
			// This is more generic than the above code - we just want it to throw an exception 
			// and don't care what values are passed to it.
			_downloader.Setup(d => d.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
				.Throws<WebException>();

			// act
			var result = _installerHelper.DownloadInstaller("a", "b");
			
			// arrange
			Assert.That(result, Is.False);
		}

		[Test]
		public void DownloadInstaller_DownloadCompletes_ReturnsTrue()
		{
			// act
			var result = _installerHelper.DownloadInstaller("a", "b");

			// arrange
			Assert.That(_installerHelper.DownloadInstaller("a", "b"), Is.True);
		}
	}
}
