namespace TopCourses.Core.Models.Topic
{
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Video;

    public class TopicViewModel
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public ICollection<VideoViewModel> Videos { get; set; } = new List<VideoViewModel>();

        public ICollection<FileViewModel> Files { get; set; } = new List<FileViewModel>();
    }
}
