namespace TopCourses.Core.Models.Category
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            DataConstants.CategoryTitleMaxLength,
            MinimumLength = DataConstants.CategoryTitleMinLength)]
        public string Title { get; set; } = null!;

        public int? ParentId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
