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

        public DbSet<Topic> Topics { get; set; }

        public DbSet<CourseApplicationUser> CourseApplicationUser { get; set; }

        public DbSet<ApplicationFile> Files { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CourseApplicationUser>(u =>
            {
                u.HasKey(sc => new { sc.StudentId, sc.CourseId });

                u.HasOne(sc => sc.Student)
                .WithMany(s => s.CoursesEnrolled)
                .HasForeignKey(sc => sc.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

                u.HasOne(sc => sc.Course)
                .WithMany(s => s.Students)
                .HasForeignKey(sc => sc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            });

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

            builder.Entity<Review>(r =>
            {
                r.HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .OnDelete(DeleteBehavior.Restrict);

                r.HasOne(c => c.Course)
                .WithMany(r => r.Reviews)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Course>(c =>
            {
                c.HasOne(c => c.Category)
                .WithMany(c => c.Courses)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                c.HasOne(c => c.SubCategory)
                .WithMany(c => c.CoursesSubCategories)
                .HasForeignKey(c => c.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Category>(c =>
            {
                c.HasMany(c => c.Courses)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

                c.HasMany(c => c.CoursesSubCategories)
                .WithOne(c => c.SubCategory)
                .HasForeignKey(c => c.SubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.ApplyConfiguration(new RolesConfiguration());
            //builder.ApplyConfiguration(new UserRolesConfiguration());
            //builder.ApplyConfiguration(new CategoriesConfiguration());
            //builder.ApplyConfiguration(new LanguagesConfiguration());
            //builder.ApplyConfiguration(new CoursesConfiguration());
            //builder.ApplyConfiguration(new VideoConfiguration());
            //builder.ApplyConfiguration(new TopicsConfiguration());

            base.OnModelCreating(builder);
        }
    }
}