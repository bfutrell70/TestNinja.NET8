using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.NET8.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader = default!;
        private IVideoRepository _videoRepository;

        public VideoService(IFileReader fileReader, IVideoRepository videoRepository)
        {
            _fileReader = fileReader;
            _videoRepository = videoRepository;
        }

        public string? ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();

			foreach (var v in videos)
				videoIds.Add(v.Id);

            return string.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        // In order to use Moq.EntityFrameworkCore, I had to add
        // the virtual keyword to the Videos DbSet.
        public virtual DbSet<Video> Videos { get; set; }
    }
}