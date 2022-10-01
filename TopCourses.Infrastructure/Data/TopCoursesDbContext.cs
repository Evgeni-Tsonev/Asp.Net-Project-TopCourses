namespace TopCourses.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class TopCoursesDbContext : IdentityDbContext
    {
        public TopCoursesDbContext(DbContextOptions<TopCoursesDbContext> options)
            : base(options)
        {
        }
    }
}