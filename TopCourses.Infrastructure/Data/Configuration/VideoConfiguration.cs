namespace TopCourses.Infrastructure.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;
    using TopCourses.Infrastructure.Data.Models;

    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasData(this.CreateVideos());
        }

        private List<Video> CreateVideos()
        {
            var videos = new List<Video>()
            {
                new Video()
                         {
                             Id = 1,
                             Title = "Intro",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 1,
                         },
                         new Video()
                         {
                             Id = 2,
                             Title = "First Video",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 1,
                         },
                         new Video()
                         {
                             Id = 3,
                             Title = "Other Video",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 1,
                         },
                         new Video()
                         {
                             Id = 4,
                             Title = "Intro to Topic",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 2,
                         },
                         new Video()
                         {
                             Id = 5,
                             Title = "First Video of topic",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 2,
                         },
                         new Video()
                         {
                             Id = 6,
                             Title = "Other Video",
                             Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                             TopicId = 2,
                         },
                         new Video()
                        {
                            Id = 7,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 3,
                        },
                        new Video()
                        {
                            Id = 8,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 3,
                        },
                        new Video()
                        {
                            Id = 9,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 3,
                        },
                        new Video()
                        {
                            Id = 10,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 4,
                        },
                        new Video()
                        {
                            Id = 11,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 4,
                        },
                        new Video()
                        {
                            Id = 12,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 4,
                        },
                        new Video()
                        {
                            Id = 13,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 5,
                        },
                        new Video()
                        {
                            Id = 14,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 5,
                        },
                        new Video()
                        {
                            Id = 15,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 5,
                        },
                        new Video()
                        {
                            Id = 16,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 6,
                        },
                        new Video()
                        {
                            Id = 17,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 6,
                        },
                        new Video()
                        {
                            Id = 18,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 6,
                        },
                        new Video()
                        {
                            Id = 19,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 7,
                        },
                        new Video()
                        {
                            Id = 20,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 7,
                        },
                        new Video()
                        {
                            Id = 21,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 7,
                        },
                        new Video()
                        {
                            Id = 22,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 8,
                        },
                        new Video()
                        {
                            Id = 23,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 8,
                        },
                        new Video()
                        {
                            Id = 24,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 8,
                        },
                        new Video()
                        {
                            Id = 25,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 26,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 27,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 28,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 29,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 30,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 31,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 32,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 33,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 9,
                        },
                        new Video()
                        {
                            Id = 34,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 35,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 36,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 10,
                        },
                        new Video()
                        {
                            Id = 37,
                            Title = "Intro",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 11,
                        },
                        new Video()
                        {
                            Id = 38,
                            Title = "First Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 11,
                        },
                        new Video()
                        {
                            Id = 39,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 11,
                        },
                        new Video()
                        {
                            Id = 40,
                            Title = "Intro to Topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 12,
                        },
                        new Video()
                        {
                            Id = 41,
                            Title = "First Video of topic",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 12,
                        },
                        new Video()
                        {
                            Id = 42,
                            Title = "Other Video",
                            Url = "https://www.youtube.com/embed/P-uxPyF9Icg?autoplay=1",
                            TopicId = 12,
                        },
            };

            return videos;
        }
    }
}
