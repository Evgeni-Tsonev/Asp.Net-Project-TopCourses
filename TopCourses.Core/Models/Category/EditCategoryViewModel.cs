namespace TopCourses.Core.Models.Category
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            DataConstants.CategoryTitleMaxLength,
            MinimumLength = DataConstants.CategoryTitleMinLength)]
        public string Title { get; set; } = null!;
    }
}
