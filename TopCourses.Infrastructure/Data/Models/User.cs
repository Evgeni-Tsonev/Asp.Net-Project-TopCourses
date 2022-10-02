namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Course> CoursesCreated { get; set; } = new HashSet<Course>();

        public ICollection<Course> CoursesEnrolled { get; set; } = new HashSet<Course>();
    }
}