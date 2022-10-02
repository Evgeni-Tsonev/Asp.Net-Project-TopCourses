namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Language
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}