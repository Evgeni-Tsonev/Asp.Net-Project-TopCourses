namespace TopCourses.Tests
{
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Services;
    using TopCourses.Infrastructure.Data;
    using TopCourses.Infrastructure.Data.Identity;
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var dbCourse = await this.courseService.GetCourseDetails(1);

            Assert.That(dbCourse, Is.Not.Null);
        }

        [Test]
        public async Task GetCourseDetailsShouldThrowExceptionTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.GetCourseDetails(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task CreateCourseTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

            var course = new AddCourseViewModel()
            {
                Title = "First",
                Subtitle = "",
                Image = new byte[0],
                Goals = "",
                Requirements = "",
                Level = Level.All,
                Price = 10m,
                Description = "",
            };

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.courseService.CreateCourse(course, "2");
            await this.repository.SaveChangesAsync();

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse, Is.Not.Null);
        }

        [Test]
        public async Task GetCourseToEditTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var dbCourse = await this.courseService.GetCourseToEdit(1);

            Assert.That(dbCourse, Is.Not.Null);
        }

        [Test]
        public async Task GetCourseToEditShouldThrowExceptionTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.GetCourseToEdit(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task UpdateCourseTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

            var course = new Course()
            {
                Id = 1,
                Title = "Title",
                Subtitle = "",
                Image = new byte[0],
                Goals = "",
                Requirements = "",
                Level = Level.All,
                Price = 10m,
                Description = "",
                CreatorId = "2"
            };

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.Update(new EditCourseViewModel()
            {
                Id = 1,
                Title = "Updated",
                Subtitle = "",
                Image = new byte[0],
                Goals = "",
                Requirements = "",
                Level = Level.All,
                Price = 10m,
                Description = "",
            }, "2");

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse.Title, Is.EqualTo("Updated"));
        }

        [Test]
        public async Task UpdateCourseInvalidCourseIdShouldThrowExceptionTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.Update(new EditCourseViewModel()
                {
                    Id = 2,
                    Title = "Updated",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                }, "2"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task UpdateCourseInvalidUserIdShouldThrowExceptionTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.Update(new EditCourseViewModel()
                {
                    Id = 1,
                    Title = "Updated",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                }, "1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task UpdateCourseUnoutorizedUserThrowExceptionTest()
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

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var user2 = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddAsync(user2);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await this.courseService.Update(new EditCourseViewModel()
                {
                    Id = 1,
                    Title = "Updated",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                }, "1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UnautorizedUser));
        }

        [Test]
        public async Task GetAllNotApprovedTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = false,
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
                    CreatorId = "2",
                    IsApproved = false,
                }
            };

            var user = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(user);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var notApprovedCourses = await this.courseService.GetAllNotApproved();

            Assert.That(notApprovedCourses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllEnroledCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
                    Students = new List<CourseApplicationUser>()
                    {
                        new CourseApplicationUser()
                        {
                            CourseId = 1,
                            StudentId = "1",
                        }
                    }
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
                    CreatorId = "2",
                    IsApproved = false,
                    Students = new List<CourseApplicationUser>()
                    {
                        new CourseApplicationUser()
                        {
                            CourseId = 2,
                            StudentId = "1",
                        }
                    }
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
                    CreatorId = "2",
                    IsApproved = false,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var student = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(student);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var allEnrolledCourses = await this.courseService.GetAllEnroledCourses("1");

            Assert.That(allEnrolledCourses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllEnroledCoursesInvalidUserIdShouldThrowExceptionTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
                    Students = new List<CourseApplicationUser>()
                    {
                        new CourseApplicationUser()
                        {
                            CourseId = 1,
                            StudentId = "1",
                        }
                    }
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
                    CreatorId = "2",
                    IsApproved = false,
                    Students = new List<CourseApplicationUser>()
                    {
                        new CourseApplicationUser()
                        {
                            CourseId = 2,
                            StudentId = "1",
                        }
                    }
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
                    CreatorId = "2",
                    IsApproved = false,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var student = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(student);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.GetAllEnroledCourses("3"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task GetAllCreatedCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var allCreatedCourses = await this.courseService.GetAllCreatedCourses("2");

            Assert.That(allCreatedCourses.Count, Is.EqualTo(3));
        }

        [Test]
        public async Task GetAllCreatedInvalidUerIdShouldThrowExceptionCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.GetAllCreatedCourses("1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task GetAllArchivedCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = false,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var allCreatedCourses = await this.courseService.GetAllArchivedCourses("2");

            Assert.That(allCreatedCourses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllArchivedCoursesInvalidUerIdShouldThrowExceptionCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.GetAllArchivedCourses("1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task AddStudentToCourseTest()
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
                IsApproved = true,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var student = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(student);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.AddStudentToCourse(1, "1");

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse.Students.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task AddStudentToCourseInvalidCourseIdShouldThrowExceptionTest()
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
                IsApproved = true,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var student = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(student);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.AddStudentToCourse(2, "1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task AddStudentToCourseInvalidUserIdShouldThrowExceptionTest()
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
                IsApproved = true,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var student = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(student);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.AddStudentToCourse(1, "4"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task ApproveCourseTest()
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
                IsApproved = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.ApproveCourse(1);

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse.IsApproved, Is.EqualTo(true));
        }

        [Test]
        public async Task ApproveCourseInvalidCourseIdShouldThworExceptionTest()
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
                IsApproved = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.ApproveCourse(1);

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.ApproveCourse(2));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task DeleteCourseTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.Delete(1, "2");

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse.IsDeleted, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteCourseInvalidUserIdShouldThrowExceptionTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.Delete(1, "1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task DeleteCourseInvalidCourseIdShouldThrowExceptionTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.Delete(2, "2"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.CourseNotExists));
        }

        [Test]
        public async Task DeleteCourseByAdminTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.Delete(1, "1", true);

            var dbCourse = await this.repository.GetByIdAsync<Course>(1);

            Assert.That(dbCourse.IsDeleted, Is.EqualTo(true));
        }

        [Test]
        public async Task DeleteCourseByUserNotCreatorShouldThrowExceptionTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            await this.courseService.Delete(1, "1", true);

            var ex = Assert.ThrowsAsync<UnauthorizedAccessException>(
                async () => await this.courseService.Delete(1, "1"));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UnautorizedUser));
        }

        [Test]
        public async Task DoUserHavePermissionTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var doUserHavePermission = await this.courseService.DoUserHavePermission("2", 1);

            Assert.That(doUserHavePermission, Is.EqualTo(true));
        }

        [Test]
        public async Task DoUserHavePermissionShouldReturnFalseTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            var user = new ApplicationUser()
            {
                Id = "1",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(user);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var doUserHavePermission = await this.courseService.DoUserHavePermission("1", 1);

            Assert.That(doUserHavePermission, Is.EqualTo(false));
        }

        [Test]
        public async Task DoUserHavePermissionInvalidUserIdShouldThrowExceptionTest()
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
                IsDeleted = false,
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();

            var ex = Assert.ThrowsAsync<ArgumentException>(
                async () => await this.courseService.DoUserHavePermission("1", 1));

            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.UserNotExists));
        }

        [Test]
        public async Task GetRandomCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
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
                    CreatorId = "2",
                    IsApproved = true,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var randomCourses = await this.courseService.GetRandomCourses(2);

            Assert.That(randomCourses.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task GetAllCoursesTest()
        {
            var videoServiceMock = new Mock<IVideoService>();
            var videoService = videoServiceMock.Object;

            this.repository = new DbRepository(this.context);
            this.courseService = new CourseService(this.repository, videoService);

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
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = false,
                },
                new Course()
                {
                    Id = 2,
                    Title = "second",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = false,
                },
                new Course()
                {
                    Id = 3,
                    Title = "third",
                    Subtitle = "",
                    Image = new byte[0],
                    Goals = "",
                    Requirements = "",
                    Level = Level.All,
                    Price = 10m,
                    Description = "",
                    CreatorId = "2",
                    IsApproved = true,
                    IsDeleted = false,
                }
            };

            var creator = new ApplicationUser()
            {
                Id = "2",
                FirstName = "",
                LastName = "",
                ProfileImage = new byte[0],
            };

            await this.repository.AddAsync(creator);
            await this.repository.AddRangeAsync(courses);
            await this.repository.SaveChangesAsync();

            var allCourses = await this.courseService.GetAll(null, null, null,null, 0, 100, 1, 10);

            Assert.That(allCourses.Count, Is.EqualTo(3));
        }

        [TearDown]
        public void TearDown()
        {
            this.context.Dispose();
        }
    }
}
