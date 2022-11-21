namespace TopCourses.Core.Models.Video
{
    public class VideoViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string VideoUrl { get; set; } = null!;

        public string? TopicTitle { get; set; }

        public string? CourseTitle { get; set; }
    }
}
