namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Models.enums;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Models.Section;

    public class CourseDetailsModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Subtitle { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Goals = null!;

        [Required]
        [StringLength(1000)]
        public string Requirements = null!;

        public ICollection<SectionModel> Curriculum { get; set; } = new HashSet<SectionModel>();

        public Level Level { get; set; }

        public int CategoryId { get; set; }

        public int LanguageId { get; set; }

        [Required]
        [StringLength(1500)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
