namespace TopCourses.Core.Models.Category
{
    using System.ComponentModel.DataAnnotations;

    public class EditCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Title { get; set; } = null!;
    }
}
