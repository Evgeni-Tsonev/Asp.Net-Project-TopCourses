namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Identity;

    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(this.CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@abv.bg",
                NormalizedEmail = "ADMIN@ABV.BG",
                FirstName = "Admin",
                LastName = "Admin",
                ProfileImage = new byte[0],
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "admin123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "regularUser",
                NormalizedUserName = "REGULARUSER",
                Email = "user@abv.bg",
                NormalizedEmail = "USER@ABV.BG",
                FirstName = "User",
                LastName = "User",
                ProfileImage = new byte[0],
            };

            user.PasswordHash =
            hasher.HashPassword(user, "user123");

            users.Add(user);

            return users;
        }
    }
}
