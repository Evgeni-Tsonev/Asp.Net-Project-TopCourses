namespace TopCourses.Core.Models.Topic
{
    using TopCourses.Core.Models.Video;

    public class TopicViewModel
    {
        public string Title { get; set; } = null!;

        //to do
        //public File Resources { get; set; }

        public string Description { get; set; } = null!;

        public ICollection<VideoViewModel> Videos { get; set; } = new List<VideoViewModel>();
    }
}
