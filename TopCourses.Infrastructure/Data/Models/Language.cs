namespace TopCourses.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class Language
    {
        public int Id { get; set; }

        [Required]
        [StringLength(DataConstants.LanguageTitleMaxLength)]
        public string Title { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}