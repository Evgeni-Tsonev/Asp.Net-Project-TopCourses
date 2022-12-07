namespace TopCourses.Tests
{
    using Moq;
    using TopCourses.Core.Data.Common;
    using TopCourses.Infrastructure.Data;

    public class UnitTestBase
    {
        //protected TopCoursesTestDb testDb;
        private TopCoursesDbContext dbContext;
        protected IDbRepository repository;
    }
}
