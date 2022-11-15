namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Video
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Url]
        public string Url { get; set; } = null!;

        public int TopicId { get; set; }
        public Topic Topic { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
