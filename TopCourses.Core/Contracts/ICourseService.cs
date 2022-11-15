namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Models;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingViewModel>> GetAll();

        Task CreateCourse(AddCourseViewModel courseModel, string sreatorId);

        Task<Course> GetCourseById(int courseId);

        Task<CourseDetailsViewModel> GetCourseDetails(int courseId);

        Task AddStudentToCourse(int courseId, string studentId);
    }
}
