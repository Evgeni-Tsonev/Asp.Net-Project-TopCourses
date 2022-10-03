namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Requirement
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
