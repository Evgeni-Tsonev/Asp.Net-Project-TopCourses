namespace TopCourses.Core.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LanguageModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;
    }
}
