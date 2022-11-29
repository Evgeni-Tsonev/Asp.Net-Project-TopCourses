namespace TopCourses.Models
{
    using TopCourses.Core.Models.Course;

    public class MyLearningViewModel
    {
        public IEnumerable<CourseListingViewModel> CoursesEnrolled { get; set; } = new List<CourseListingViewModel>();

        public IEnumerable<CourseListingViewModel> CoursesCreated { get; set; } = new List<CourseListingViewModel>();
    }
}
