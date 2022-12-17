namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using TopCourses.Infrastructure.Data.Models;

    public class TopicsConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasData(this.CreateTopics());
        }

        private List<Topic> CreateTopics()
        {
            var topics = new List<Topic>()
            {
                new Topic()
                {
                     Id = 1,
                     Title = "Introduction",
                     CourseId = 1,
                     Description = "very long description...",
                },
                new Topic()
                {
                     Id = 2,
                     Title = "First Topic",
                     CourseId = 1,
                     Description = "very long description...",
                },
                new Topic()
                {
                    Id = 3,
                    Title = "Introduction",
                    CourseId = 2,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 4,
                    Title = "First Topic",
                    CourseId = 2,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 5,
                    Title = "Introduction",
                    CourseId = 3,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 6,
                    Title = "First Topic",
                    CourseId = 3,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 7,
                    Title = "Introduction",
                    CourseId = 4,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 8,
                    Title = "First Topic",
                    CourseId = 4,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 9,
                    Title = "Introduction",
                    CourseId = 5,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 10,
                    Title = "First Topic",
                    CourseId = 5,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 11,
                    Title = "Introduction",
                    CourseId = 6,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 12,
                    Title = "First Topic",
                    CourseId = 6,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 13,
                    Title = "Introduction",
                    CourseId = 7,
                    Description = "very long description...",
                },
                new Topic()
                {
                    Id = 14,
                    Title = "First Topic",
                    CourseId = 7,
                    Description = "very long description...",
                },
            };

            return topics;
        }
    }
}
