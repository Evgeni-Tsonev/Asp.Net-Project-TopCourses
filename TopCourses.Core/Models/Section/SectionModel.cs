namespace TopCourses.Core.Models.Section
{
    using System.ComponentModel.DataAnnotations;

    public class SectionModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [Url]
        public string VideoUrl { get; set; } = null!;

        //to do
        //public File Resources { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;
    }
}
