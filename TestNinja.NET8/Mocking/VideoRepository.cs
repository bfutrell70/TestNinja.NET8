using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.NET8.Mocking
{
	public class VideoRepository: IVideoRepository
	{
		private VideoContext _videoContext;

        public VideoRepository(VideoContext videoContext)
        {
            _videoContext = videoContext;
        }

        public IEnumerable<Video>? GetUnprocessedVideos()
		{
			var videos =
					(from video in _videoContext.Videos
					 where !video.IsProcessed
					 select video).ToList();

			return videos;
		}
	}
}
