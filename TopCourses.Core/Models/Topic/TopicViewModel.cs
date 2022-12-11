namespace TopCourses.Core.Models.Topic
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Video;

    public class TopicViewModel
    {
        public int Id { get; set; }

        //todo
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        //todo
        public string Description { get; set; } = null!;

        public ICollection<VideoViewModel> Videos { get; set; } = new List<VideoViewModel>();

        public ICollection<FileViewModel> Files { get; set; } = new List<FileViewModel>();
    }
}
