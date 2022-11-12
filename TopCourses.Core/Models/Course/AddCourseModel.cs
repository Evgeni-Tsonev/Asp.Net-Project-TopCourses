namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.Language;
    using TopCourses.Core.Models.Section;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class AddCourseModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Subtitle { get; set; } = null!;

        [Required]
        [Url]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Goals = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Requirements = null!;

        public AddSectionModel? Section { get; set; }
        public ICollection<AddSectionModel> Curriculum { get; set; } = new HashSet<AddSectionModel>();

        public Level Level { get; set; }

        public int CategoryId { get; set; }

        public int LanguageId { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; } = new HashSet<LanguageViewModel>();

        public IEnumerable<CategoryModel> Categories { get; set; } = new HashSet<CategoryModel>();
    }
}
