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
        public DbSet<Course> Courses { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Language> Languages { get; set; } = null!;
        public DbSet<Section> Sections { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; } = null!;
        public DbSet<Requirement> Requirements { get; set; } = null!;
        public DbSet<Goal> Goals { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}