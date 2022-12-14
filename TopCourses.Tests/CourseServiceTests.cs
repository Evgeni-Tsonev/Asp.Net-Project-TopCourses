namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class CourseServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private ICourseService courseService;
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
        public async Task GetCourseDetailsTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                CreatorId = "2",
            };

            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var categoryForEdit = await this.courseService.GetCourseDetails(1);

            Assert.That(categoryForEdit, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
