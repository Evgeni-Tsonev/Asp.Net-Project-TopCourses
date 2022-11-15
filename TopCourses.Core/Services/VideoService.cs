namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Video;

    public class VideoService : IVideoService
    {
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
                    throw new InvalidOperationException("Failed to match VideoUrl Id");
                }

                processedVideos.Add(video);
            }

            return processedVideos;
        }
    }
}
