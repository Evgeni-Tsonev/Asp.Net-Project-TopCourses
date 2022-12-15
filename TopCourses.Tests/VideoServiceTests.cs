namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Video;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Infrastructure.Data.Models.enums;

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
                Id = 1,
                Title = "First",
                Url = "",
                TopicId = 1,
                IsDeleted = false,
            };

            var topic = new Topic()
            {
                Id = 1,
                Title = "",
                Description = "",
                CourseId = 1,
            };

            var course = new Course()
            {
                Id = 1,
                Title = "First",
                Subtitle = "",
                Image = new byte[0],
                Goals = "",
                Requirements = "",
                Level = Level.All,
                Price = 10m,
                Description = "",
                CreatorId = "1",
                IsApproved = true,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(topic);
            await this.repository.AddAsync(course);
            await this.repository.AddAsync(video);
            await this.repository.SaveChangesAsync();

            var dbVideo = await this.videoService.GetVideoById(1);

            Assert.That(dbVideo, Is.Not.Null);
        }

        [Test]
        public async Task GetVideoByInvalidIdShouldThrolExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.videoService = new VideoService(this.repository);
            var video = new Video()
            {
                Id = 1,
                Title = "First",
                Url = "",
                TopicId = 1,
                IsDeleted = false,
            };

            var topic = new Topic()
            {
                Id = 1,
                Title = "",
                Description = "",
                CourseId = 1,
            };

            var course = new Course()
            {
                Id = 1,
                Title = "First",
                Subtitle = "",
                Image = new byte[0],
                Goals = "",
                Requirements = "",
                Level = Level.All,
                Price = 10m,
                Description = "",
                CreatorId = "1",
                IsApproved = true,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(topic);
            await this.repository.AddAsync(course);
            await this.repository.AddAsync(video);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.videoService.GetVideoById(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.VideoNotExists));
        }

        [Test]
        public async Task ReplaceVideoUrlsTest()
        {
            this.repository = new DbRepository(this.context);
            this.videoService = new VideoService(this.repository);
            var videos = new List<AddVideoViewModel>()
            {
                new AddVideoViewModel()
                {
                    Title = "",
                    VideoUrl = "https://www.youtube.com/watch?v=06rKoMVSZrg"
                },
                new AddVideoViewModel()
                {
                    Title = "",
                    VideoUrl = "https://www.youtube.com/watch?v=06rKoMVSZrg"
                }
            };

            var returnedVideos = await this.videoService.ReplaceVideoUrls(videos);
            var firstVideo = returnedVideos.FirstOrDefault();
            Assert.Multiple(() =>
            {
                Assert.That(returnedVideos.Count(), Is.EqualTo(2));
                Assert.That(firstVideo.VideoUrl, Is.EqualTo("https://www.youtube.com/embed/06rKoMVSZrg?autoplay=1"));
            });
        }

        [Test]
        public async Task ReplaceVideoUrlsInvalidUrlShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.videoService = new VideoService(this.repository);
            var videos = new List<AddVideoViewModel>()
            {
                new AddVideoViewModel()
                {
                    Title = "",
                    VideoUrl = "https://www.invalid.com/watch?v=wrongUrl"
                },
                new AddVideoViewModel()
                {
                    Title = "",
                    VideoUrl = "https://www.youtube.com/watch?v=06rKoMVSZrg"
                }
            };

            var ex = Assert.ThrowsAsync<InvalidOperationException>(
                async () => await this.videoService.ReplaceVideoUrls(videos));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.FailedToMatchVideoUrl));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
