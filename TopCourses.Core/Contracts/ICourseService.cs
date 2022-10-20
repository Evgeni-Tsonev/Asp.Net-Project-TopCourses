namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Models;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingModel>> GetAll();

        Task CreateCourse(AddCourseModel courseModel, string sreatorId);

        Task<Course> GetCourseById(int courseId);

        Task<CourseDetailsModel> GetCourseDetails(int courseId);
    }
}
