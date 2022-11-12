namespace TopCourses.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class TopCoursesDbContext : IdentityDbContext<ApplicationUser>
    {
        public TopCoursesDbContext(DbContextOptions<TopCoursesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<CourseApplicationUser> CourseApplicationUser { get; set; }
        public DbSet<ApplicationFile> Files { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseApplicationUser>()
                .HasKey(sc => new { sc.StudentId, sc.CourseId });

            builder.Entity<CourseApplicationUser>()
                .HasOne<ApplicationUser>(sc => sc.Student)
                .WithMany(s => s.CoursesEnrolled)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<CourseApplicationUser>()
                .HasOne<Course>(sc => sc.Course)
                .WithMany(s => s.Students)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShoppingCart>()
                   .HasOne(x => x.User)
                   .WithOne(x => x.ShoppingCart)
                   .HasForeignKey<ApplicationUser>(x => x.ShoppingCartId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                   .HasOne(x => x.ShoppingCart)
                   .WithOne(x => x.User)
                   .HasForeignKey<ShoppingCart>(x => x.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}