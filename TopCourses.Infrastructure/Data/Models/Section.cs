namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Section
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Url]
        public string? VideoUrl { get; set; }

        public int? ResourceId { get; set; }
        public ApplicationFile? Resource { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
