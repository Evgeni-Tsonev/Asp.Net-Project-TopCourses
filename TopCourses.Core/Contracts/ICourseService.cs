namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Models;

    public interface ICourseService
    {
        Task<IEnumerable<CourseListingViewModel>> GetAll(
            string? category = null,
            string? subCategory = null,
            string? searchTerm = null,
            string? language = null,
            decimal? minPrice = 0,
            decimal? maxPrice = 2000,
            int currentPage = 1,
            int coursessPerPage = 1,
            CourseSorting sorting = CourseSorting.Newest);

        Task<IEnumerable<CourseListingViewModel>> GetAllNotApproved();

        Task<IEnumerable<CourseListingViewModel>> GetAllEnroledCourses(string userId);

        Task<IEnumerable<CourseListingViewModel>> GetAllCreatedCourses(string userId);

        Task Delete(int courseId, string userId);

        Task CreateCourse(AddCourseViewModel courseModel, string sreatorId);

        Task<Course> GetCourseById(int courseId);

        Task ApproveCourse(int courseId);

        Task<CourseDetailsViewModel> GetCourseDetails(int courseId);

        Task AddStudentToCourse(int courseId, string studentId);
    }
}
