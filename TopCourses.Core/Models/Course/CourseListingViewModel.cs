namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseListingViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        [Url]
        public string ImageUrl { get; set; } = null!;

        public decimal Price { get; set; }

        public double Rating { get; set; }
    }
}
