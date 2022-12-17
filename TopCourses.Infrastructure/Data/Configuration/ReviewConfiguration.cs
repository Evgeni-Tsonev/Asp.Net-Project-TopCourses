namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Models;

    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(this.CreateReviews());
        }

        private List<Review> CreateReviews()
        {
            var reviews = new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    Rating = 5,
                    Comment = "The best C# course",
                    CourseId = 1,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 2,
                    Rating = 4,
                    Comment = "Not The best C# course",
                    CourseId = 1,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 3,
                    Rating = 1,
                    Comment = "So bad course",
                    CourseId = 1,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 4,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 1,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 5,
                    Rating = 5,
                    Comment = "The best C# course",
                    CourseId = 2,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 6,
                    Rating = 3,
                    Comment = "Not The best C# course",
                    CourseId = 2,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 7,
                    Rating = 3,
                    Comment = "So bad course",
                    CourseId = 2,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 8,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 2,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 9,
                    Rating = 5,
                    Comment = "The best C# course",
                    CourseId = 3,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 10,
                    Rating = 3,
                    Comment = "Not The best C# course",
                    CourseId = 3,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 11,
                    Rating = 3,
                    Comment = "So bad course",
                    CourseId = 3,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 12,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 3,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 13,
                    Rating = 5,
                    Comment = "The best C# course",
                    CourseId = 4,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 14,
                    Rating = 3,
                    Comment = "Not The best C# course",
                    CourseId = 4,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 15,
                    Rating = 3,
                    Comment = "So bad course",
                    CourseId = 4,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 16,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 4,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 17,
                    Rating = 5,
                    Comment = "The best C# course",
                    CourseId = 6,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 18,
                    Rating = 3,
                    Comment = "Not The best C# course",
                    CourseId = 6,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 19,
                    Rating = 3,
                    Comment = "So bad course",
                    CourseId = 6,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 20,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 6,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 21,
                    Rating = 5,
                    Comment = "The best course",
                    CourseId = 7,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 22,
                    Rating = 3,
                    Comment = "Not the best C# course",
                    CourseId = 7,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 23,
                    Rating = 3,
                    Comment = "So bad course",
                    CourseId = 7,
                    UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                    DateOfPublication = DateTime.Now,
                },
                new Review()
                {
                    Id = 24,
                    Rating = 3,
                    Comment = "Good course",
                    CourseId = 7,
                    UserId = "dea12856-c198-4129-b3f3-b893d8395082",
                    DateOfPublication = DateTime.Now,
                },
            };

            return reviews;
        }
    }
}
