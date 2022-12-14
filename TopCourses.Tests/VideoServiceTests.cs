namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Models;

    public class VideoServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IVideoService videoService;

        [SetUp]
        public void Setup()
        {
            var contextOptopns = new DbContextOptionsBuilder<TopCoursesDbContext>()
                .UseInMemoryDatabase("TopCourses")
                .Options;

            this.context = new TopCoursesDbContext(contextOptopns);
            this.context.Database.EnsureDeleted();
            this.context.Database.EnsureCreated();
        }

        [Test]
        public async Task GetVideoByIdTest()
        {
            this.repository = new DbRepository(this.context);
            this.videoService = new VideoService(this.repository);
            var video = new Video()
            {
                Id = 3,
                Title = "First",
                Url = "",
                TopicId = 2,
                IsDeleted = false,
            };

            await this.repository.AddAsync(video);
            await this.repository.SaveChangesAsync();

            var dbVideo = await this.videoService.GetVideoById(3);

            Assert.That(dbVideo, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
