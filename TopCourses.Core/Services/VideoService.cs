namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Video;
    using TopCourses.Infrastructure.Data.Models;

    public class VideoService : IVideoService
    {
        private readonly IDbRepository repository;

        public VideoService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task<VideoViewModel> GetVideoById(int id)
        {
            var video = await this.repository.AllReadonly<Video>()
                .Where(v => v.Id == id)
                .Include(v => v.Topic)
                .ThenInclude(t => t.Course)
                .FirstOrDefaultAsync();

            if (video == null)
            {
                throw new ArgumentException(ExceptionMessages.VideoNotExists);
            }

            return new VideoViewModel()
            {
                Id = video.Id,
                Title = video.Title,
                VideoUrl = video.Url,
                TopicTitle = video.Topic.Title,
                CourseTitle = video.Topic.Course.Title,
            };
        }

        public async Task<ICollection<AddVideoViewModel>> ReplaceVideoUrls(IList<AddVideoViewModel> videos)
        {
            var processedVideos = new List<AddVideoViewModel>();

            foreach (var video in videos)
            {
                var pattern = @"http(?:s)?:\/\/(?:m.)?(?:www\.)?youtu(?:\.be\/|(?:be-nocookie|be)\.com\/(?:watch|[\w]+\?(?:feature=[\w]+.[\w]+\&)?v=|v\/|e\/|embed\/|user\/(?:[\w#]+\/)+))([^&#?\n]+)";

                var videoIdGroup = 1;

                var regex = new Regex(pattern);
                var match = regex.Match(video.VideoUrl);
                if (match.Success)
                {
                    var videoId = match.Groups[videoIdGroup];
                    var processedVideoUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";
                    video.VideoUrl = processedVideoUrl;
                }
                else
                {
                    throw new InvalidOperationException(ExceptionMessages.FailedToMatchVideoUrl);
                }

                processedVideos.Add(video);
            }

            return processedVideos;
        }
    }
}
