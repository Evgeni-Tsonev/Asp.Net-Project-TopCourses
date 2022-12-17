namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(new IdentityRole()
            {
                Id = "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4",
                ConcurrencyStamp = "bd2e6c29-be4d-45ea-9fee-a0cffdf530d4",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            });
        }
    }
}
