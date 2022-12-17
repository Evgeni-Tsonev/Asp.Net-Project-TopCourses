namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Infrastructure.Data.Models.enums;

    internal class CoursesConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasData(this.CreateCourses());
        }

        private List<Course> CreateCourses()
        {
            List<Course> courses = new List<Course>()
            {
                new Course()
                {
                    Id = 1,
                    Title = "The Complete Web Devolopment Bootcamp",
                    Subtitle = "The Complete C# Web Devolopment Bootcamp",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 1,
                    SubCategoryId = 5,
                    LanguageId = 1,
                    Description = "this is very long description.....",
                    Price = 99.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 2,
                    Title = "The Complete React Course",
                    Subtitle = "The Best React Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 1,
                    SubCategoryId = 5,
                    LanguageId = 1,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 3,
                    Title = "The Complete Angular Course",
                    Subtitle = "The Best Angular Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 1,
                    SubCategoryId = 6,
                    LanguageId = 2,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 4,
                    Title = "The Complete Data Structures Course",
                    Subtitle = "The Best Data Structures Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 1,
                    SubCategoryId = 6,
                    LanguageId = 2,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 5,
                    Title = "The Complete Mobile Development Course",
                    Subtitle = "The Best Mobile Development Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 1,
                    SubCategoryId = 6,
                    LanguageId = 2,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 6,
                    Title = "The Complete Mobile Development Course",
                    Subtitle = "The Best Mobile Development Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 2,
                    SubCategoryId = 8,
                    LanguageId = 1,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
                new Course()
                {
                    Id = 7,
                    Title = "The Complete Mobile Development Course",
                    Subtitle = "The Best Mobile Development Course",
                    Image = new byte[0],
                    Goals = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Requirements = "ikasncialsuhcasuicvauivavkajsnvanasc",
                    Level = Level.Beginner,
                    CategoryId = 2,
                    SubCategoryId = 9,
                    LanguageId = 1,
                    Description = "this is very long description.....",
                    Price = 75.99m,
                    CreatorId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    IsApproved = true,
                    CreatedOn = DateTime.Now,
                },
            };

            return courses;
        }
    }
}
