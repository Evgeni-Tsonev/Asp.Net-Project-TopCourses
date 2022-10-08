namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Identity;

    public class CourseApplicationUser
    {
        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        [Required]
        public string StudentId { get; set; } = null!;
        public ApplicationUser Student { get; set; } = null!;
    }
}
