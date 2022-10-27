namespace TopCourses.Core.Models.Section
{
    using System.ComponentModel.DataAnnotations;

    public class SectionModel
    {
        [Required]
        [StringLength(50)]
        public string SectionTitle { get; set; } = null!;

        [Required]
        [Url]
        public string VideoUrl { get; set; } = null!;

        //to do
        //public File Resources { get; set; }

        [Required]
        [StringLength(1000)]
        public string SectionDescription { get; set; } = null!;
    }
}
