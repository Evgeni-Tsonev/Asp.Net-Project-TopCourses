namespace TopCourses.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection.Emit;
    using TopCourses.Infrastructure.Data.Models;

    public class TopCoursesDbContext : IdentityDbContext
    {
        public TopCoursesDbContext(DbContextOptions<TopCoursesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Requirement> Requirements { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(c => c.CoursesCreated)
                .WithOne(c => c.Creator)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<User>()
                .HasMany(c => c.CoursesEnrolled)
                .WithMany(c => c.Students);

            base.OnModelCreating(builder);
        }
    }
}