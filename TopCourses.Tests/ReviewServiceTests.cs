namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Review;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class ReviewServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IReviewService reviewService;

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
        public async Task AddReviewTest()
        {
            this.repository = new DbRepository(this.context);
            this.reviewService = new ReviewService(this.repository);
            await this.repository.AddAsync(new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = ""
            });

            await this.repository.AddAsync(new Course()
            {
                Id = 5,
            });

            await this.reviewService
                .AddReview(new AddReviewViewModel()
                {
                    Comment = "comment",
                    Rating = 5,
                    UserId = "1",
                    CourseId = 5,
                });

            await this.repository.SaveChangesAsync();

            var dbReview = await this.repository.GetByIdAsync<Review>(1);

            Assert.That(dbReview, Is.Not.Null);
            Assert.That(dbReview.Comment, Is.EqualTo("Comment"));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
