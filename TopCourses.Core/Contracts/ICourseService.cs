namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingModel>> GetAll();

        Task CreateCourse(AddCourseModel courseModel);

        Task<CourseDetailsModel> GetCourseDetails(int courseId);
    }
}
