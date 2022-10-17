namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class AddCourseModel
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

        public ICollection<Requirement> Requirements { get; set; } = new HashSet<Requirement>();

        public ICollection<Goal> Goals { get; set; } = new HashSet<Goal>();

        public ICollection<Section> Curriculum { get; set; } = new HashSet<Section>();

        public Level Level { get; set; }

        public int CategoryId { get; set; }

        public int LanguageId { get; set; }

        [Required]
        [StringLength(1500)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public IEnumerable<LanguageModel> Languages { get; set; } = new HashSet<LanguageModel>();
        public IEnumerable<CategoryModel> Categories { get; set; } = new HashSet<CategoryModel>();


    }
}
