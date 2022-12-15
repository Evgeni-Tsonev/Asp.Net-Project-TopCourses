namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Infrastructure.Data.Models.enums;

    public class ShoppingCartServiceTests
    {
        private IDbRepository repository;
        private TopCoursesDbContext context;
        private IShoppingCartService shoppingCartService;

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
        public async Task AddCourseInShoppingCartTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

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
                Creator = new ApplicationUser()
                {
                    Id = "2",
                    FirstName = "",
                    LastName = "",
                    ProfileImage = new byte[0],
                },
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.shoppingCartService.AddCourseInShoppingCart(1, "1");

            var dbShoppingCart = await this.repository.GetByIdAsync<ShoppingCart>(1);

            Assert.That(dbShoppingCart, Is.Not.Null);
        }

        [Test]
        public async Task AddCourseInShoppingCartInvalidUserIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

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
                Creator = new ApplicationUser()
                {
                    Id = "2",
                    FirstName = "",
                    LastName = "",
                    ProfileImage = new byte[0],
                },
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.shoppingCartService.AddCourseInShoppingCart(1, "3"));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task AddCourseInShoppingCartInvalidCourseIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

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
                Creator = new ApplicationUser()
                {
                    Id = "2",
                    FirstName = "",
                    LastName = "",
                    ProfileImage = new byte[0],
                }
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.shoppingCartService.AddCourseInShoppingCart(2, "1"));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task AddCourseInShoppingCartUserAlreadyEnrolledShouldNotThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

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
                Creator = new ApplicationUser()
                {
                    Id = "2",
                    FirstName = "",
                    LastName = "",
                    ProfileImage = new byte[0],
                }
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var userCourse = new CourseApplicationUser()
            {
                CourseId = 1,
                StudentId = "1"
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.AddAsync(userCourse);
            await this.repository.SaveChangesAsync();

            var dbUser = await this.repository
                .All<ApplicationUser>()
                .Include(x => x.CoursesEnrolled)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == "1");

            var courseEnrolled = dbUser.CoursesEnrolled.
                Select(x => x.Course)
                .FirstOrDefault(x => x.Id == 1);

            Assert.That(courseEnrolled, Is.Not.Null);
            Assert.That(courseEnrolled.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task DeleteAllCoursesFromShoppingTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var courses = new List<Course>()
            {
                new Course()
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
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 2,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 3,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                }
            };

            var cart = new ShoppingCart()
            {
                Id = 1,
                UserId = "1",
                ShoppingCartCourses = courses,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.AddAsync(cart);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            await this.shoppingCartService.DeleteAllCoursesFromShoppingCart("1");

            var dbCart = await this.repository.GetByIdAsync<ShoppingCart>(1);

            Assert.That(dbCart.ShoppingCartCourses.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task DeleteAllCoursesFromShoppingCartInvalidUserIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var courses = new List<Course>()
            {
                new Course()
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
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 2,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 3,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                }
            };

            var cart = new ShoppingCart()
            {
                Id = 1,
                UserId = "1",
                ShoppingCartCourses = courses,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.AddAsync(cart);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.shoppingCartService.DeleteAllCoursesFromShoppingCart("4"));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task DeleteCourseFromShoppingCartTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var courses = new List<Course>()
            {
                new Course()
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
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 2,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 3,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                }
            };

            var cart = new ShoppingCart()
            {
                Id = 1,
                UserId = "1",
                ShoppingCartCourses = courses,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.AddAsync(cart);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            await this.shoppingCartService.DeleteCourseFromShoppingCart(1, "1");

            var dbCart = await this.repository.GetByIdAsync<ShoppingCart>(1);

            Assert.That(dbCart.ShoppingCartCourses.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task DeleteCourseFromShoppingCartInvalidUserIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var courses = new List<Course>()
            {
                new Course()
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
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 2,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 3,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                }
            };

            var cart = new ShoppingCart()
            {
                Id = 1,
                UserId = "1",
                ShoppingCartCourses = courses,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.AddAsync(cart);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            await this.shoppingCartService.DeleteCourseFromShoppingCart(1, "1");

            var dbCart = await this.repository.GetByIdAsync<ShoppingCart>(1);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.shoppingCartService.DeleteCourseFromShoppingCart(1, "3"));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task DeleteCourseFromShoppingCartInvalidCourseIdShouldThrowExceptionTest()
        {
            this.repository = new DbRepository(this.context);
            this.shoppingCartService = new ShoppingCartService(this.repository);

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var courses = new List<Course>()
            {
                new Course()
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
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 2,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                },
                new Course()
                {
                    Id = 3,
                    Title = "First",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2"
                }
            };

            var cart = new ShoppingCart()
            {
                Id = 1,
                UserId = "1",
                ShoppingCartCourses = courses,
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.AddAsync(cart);
            await this.repository.AddAsync(user);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.shoppingCartService.DeleteCourseFromShoppingCart(4, "1"));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
