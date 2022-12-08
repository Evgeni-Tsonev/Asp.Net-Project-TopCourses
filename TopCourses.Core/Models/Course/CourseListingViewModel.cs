namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;
    using TopCourses.Core.Models.ApplicationFile;

    public class CourseListingViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;

        public ImageFileViewModel Image { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public int TotalCoursesCount { get; set; }
    }
}
