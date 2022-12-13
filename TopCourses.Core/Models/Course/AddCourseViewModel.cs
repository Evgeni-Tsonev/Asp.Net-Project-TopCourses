namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Language;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class AddCourseViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Subtitle { get; set; } = null!;

        [ValidateNever]
        public byte[] Image { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Goals { get; set; } = null!;

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string Requirements { get; set; } = null!;

        public IList<AddTopicViewModel> Curriculum { get; set; } = new List<AddTopicViewModel>();

        public Level Level { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public int SubCategoryId { get; set; }

        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 10)]
        public string Description { get; set; } = null!;

        //todo min/max
        public decimal Price { get; set; }

        public IEnumerable<LanguageViewModel> Languages { get; set; } = new HashSet<LanguageViewModel>();

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new HashSet<CategoryViewModel>();
    }
}
