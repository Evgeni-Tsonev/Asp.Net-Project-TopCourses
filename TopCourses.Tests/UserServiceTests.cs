namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.User;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class UserServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IUserService userService;

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
        public async Task GetUserByIdTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0]
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var dbUser = await this.userService.GetUserById("1");

            Assert.That(dbUser, Is.Not.Null);
        }

        [Test]
        public async Task GetUserProfileTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0]
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var dbUser = await this.userService.GetUserProfile("1");

            Assert.That(dbUser, Is.Not.Null);
        }

        [Test]
        public async Task GetUserProfileShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0]
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.userService.GetUserProfile("2"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task GetUserForEditTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0]
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var dbUser = await this.userService.GetUserForEdit("1");

            Assert.That(dbUser, Is.Not.Null);
        }

        [Test]
        public async Task GetUserForEditShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0]
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.userService.GetUserForEdit("2"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task GetUsersTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { Id = "1", FirstName = "", LastName = "", ProfileImage = new byte[0]},
                new ApplicationUser() { Id = "2", FirstName = "", LastName = "", ProfileImage = new byte[0]},
                new ApplicationUser() { Id = "3", FirstName = "", LastName = "", ProfileImage = new byte[0]},
                new ApplicationUser() { Id = "4", FirstName = "", LastName = "", ProfileImage = new byte[0]},
            };

            await this.repository.AddRangeAsync(users);
            await this.repository.SaveChangesAsync();

            var sdUsers = await this.userService.GetUsers();
            var userToAssert = sdUsers.FirstOrDefault(x => x.Id == "2");

            Assert.Multiple(() =>
            {
                Assert.That(userToAssert, Is.Not.Null);
                Assert.That(sdUsers.Count, Is.EqualTo(4));
            });
        }

        [Test]
        public async Task UpdateUserTest()
        {
            this.repository = new DbRepository(this.context);
            this.userService = new UserService(this.repository);
            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            await this.userService.UpdateUser(new UserEditViewModel()
            {
                Id = "1",
                FirstName = "Edited",
                LastName = "",
                ProfileImage = new byte[0],
            });

            var dbUser = await this.repository.GetByIdAsync<ApplicationUser>("1");

            Assert.Multiple(() =>
            {
                Assert.That(dbUser, Is.Not.Null);
                Assert.That(dbUser.FirstName, Is.EqualTo("Edited"));
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
