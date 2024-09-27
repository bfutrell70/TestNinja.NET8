using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.NET8.Mocking
{
	public class FileDownloader : IFileDownloader
	{
		public void DownloadFile(string url, string path)
		{
			var client = new HttpClient();

			var bytes = client.GetByteArrayAsync(url).Result;
			File.WriteAllBytesAsync(path, bytes).RunSynchronously();
		}
	}
}
