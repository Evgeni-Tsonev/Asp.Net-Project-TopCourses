namespace TopCourses.Core.Contracts
{
    using TopCourses.Core.Models;

    public interface ICourseService
    {
        Task<IEnumerable<CourseModel>> GetAll();
    
    }
}
