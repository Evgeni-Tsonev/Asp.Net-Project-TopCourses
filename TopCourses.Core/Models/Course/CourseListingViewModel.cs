namespace TopCourses.Core.Models.Course
{
    using System.ComponentModel.DataAnnotations;

    public class CourseListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public byte[] Image { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public int TotalCoursesCount { get; set; }
    }
}
