namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Models;

    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.CreateCategories());
        }

        private List<Category> CreateCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Title = "Development",
                },
                new Category()
                {
                    Id = 2,
                    Title = "IT and Software",
                },
                new Category()
                {
                    Id = 3,
                    Title = "Photography and video",
                },
                new Category()
                {
                    Id = 4,
                    Title = "Design",
                },
                new Category()
                {
                    Id = 5,
                    Title = "Web Development",
                    ParentId = 1,
                },
                new Category()
                {
                    Id = 6,
                    Title = "Data Science",
                    ParentId = 1,
                },
                new Category()
                {
                    Id = 7,
                    Title = "Mobile Development",
                    ParentId = 1,
                },
                new Category()
                {
                    Id = 8,
                    Title = "Hardware",
                    ParentId = 2,
                },
                new Category()
                {
                    Id = 9,
                    Title = "Network and Securiry",
                    ParentId = 2,
                },
                new Category()
                {
                    Id = 10,
                    Title = "Operating Systems and Servers",
                    ParentId = 2,
                },
                new Category()
                {
                    Id = 11,
                    Title = "Photography",
                    ParentId = 3,
                },
                new Category()
                {
                    Id = 12,
                    Title = "Video Design",
                    ParentId = 3,
                },
                new Category()
                {
                    Id = 13,
                    Title = "Web Design",
                    ParentId = 4,
                },
                new Category()
                {
                    Id = 14,
                    Title = "Game Design",
                    ParentId = 4,
                },
            };

            return categories;
        }
    }
}
