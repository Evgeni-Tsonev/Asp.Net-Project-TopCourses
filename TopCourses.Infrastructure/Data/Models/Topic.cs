namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Topic
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        public int? ResourceId { get; set; }

        public ApplicationFile? Resource { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public int CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Video> Videos { get; set; } = new HashSet<Video>();

        public ICollection<ApplicationFile> Files { get; set; } = new List<ApplicationFile>();
    }
}
