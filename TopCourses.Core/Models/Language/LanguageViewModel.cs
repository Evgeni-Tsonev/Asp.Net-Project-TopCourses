namespace TopCourses.Core.Models.Language
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Title { get; set; } = null!;
    }
}
