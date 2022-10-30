namespace TopCourses.Core.Models.Section
{
    using System.ComponentModel.DataAnnotations;

    public class SectionModel
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Url]
        public string? VideoUrl { get; set; }

        //to do
        //public File Resources { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;
    }
}
