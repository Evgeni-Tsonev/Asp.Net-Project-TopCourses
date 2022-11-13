namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Review;
    using TopCourses.Infrastructure.Data.Models;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingModel>> GetAll();

        Task CreateCourse(AddCourseModel courseModel, string sreatorId);

        Task<Course> GetCourseById(int courseId);

        Task<CourseDetailsModel> GetCourseDetails(int courseId);

        Task AddStudentToCourse(int courseId, string studentId);

        Task AddReview(AddReviewViewModel model);
    }
}
