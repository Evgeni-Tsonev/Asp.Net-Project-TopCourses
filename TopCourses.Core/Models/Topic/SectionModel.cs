namespace TopCourses.Core.Models.Topic
{

    public class SectionModel
    {
        public string Title { get; set; } = null!;

        public string? VideoUrl { get; set; }

        //public VideoModel? Video { get; set; }

        //to do
        //public File Resources { get; set; }

        public string Description { get; set; } = null!;
    }
}
