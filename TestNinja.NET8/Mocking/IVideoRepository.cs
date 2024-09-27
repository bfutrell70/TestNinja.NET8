using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.NET8.Mocking
{
	public interface IVideoRepository
	{
		IEnumerable<Video>? GetUnprocessedVideos();
	}
}
