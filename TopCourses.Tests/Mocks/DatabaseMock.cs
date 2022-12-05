namespace TopCourses.Tests.Mocks
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Infrastructure.Data;

    public static class DatabaseMock
    {
        public static TopCoursesDbContext Instance
        {
            get
            {
                var optionsBuilder = new DbContextOptionsBuilder<TopCoursesDbContext>();
                optionsBuilder.UseInMemoryDatabase($"TopCourses-TestDb-{DateTime.Now.Ticks}");

                return new TopCoursesDbContext(optionsBuilder.Options);
            }
        }
    }
}
