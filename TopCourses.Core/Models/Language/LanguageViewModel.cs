namespace TopCourses.Core.Models.Language
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Infrastructure.Data.Constants;

    public class LanguageViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(
            DataConstants.LanguageTitleMaxLength,
            MinimumLength = DataConstants.LanguageTitleMinLength)]
        public string Title { get; set; } = null!;
    }
}
