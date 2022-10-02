namespace TopCourses.Infrastructure.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class Section
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [Url]
        public string VideoUrl { get; set; } = null!;

        //to do
        //public File Resources { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
