namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Identity;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingModel>> GetAll();

        Task CreateCourse(AddCourseModel courseModel, string sreatorId);

        Task<CourseDetailsModel> GetCourseDetails(int courseId);
    }
}
