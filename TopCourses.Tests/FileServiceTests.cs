namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Models;

    [TestFixture]
    public class FileServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IFileService fileService;

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
        public async Task SaveFileTest()
        {
            this.repository = new DbRepository(this.context);
            this.fileService = new FileService(this.repository);

            await this.fileService
                .SaveFile(new ApplicationFile()
            {
                Id = 1,
                FileName = "",
                SourceId = "",
                ContentType = "",
                FileLength = 10,
            });

            var fileFromDb = this.repository.GetByIdAsync<ApplicationFile>(1);

            Assert.That(fileFromDb, Is.Not.Null);
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
