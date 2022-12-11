namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Review;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Core.Models.User;
    using TopCourses.Core.Models.Video;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class CourseService : ICourseService
    {
        private readonly IDbRepository repository;
        private readonly IVideoService videoService;

        public CourseService(IDbRepository repository, IVideoService videoService)
        {
            this.repository = repository;
            this.videoService = videoService;
        }

        public async Task<CourseDetailsViewModel> GetCourseDetails(int courseId)
        {
            var course = await this.repository
                .AllReadonly<Course>()
                .Include(r => r.Reviews)
                .ThenInclude(u => u.User)
                .Include(c => c.Curriculum)
                .ThenInclude(v => v.Videos)
                .Include(c => c.Curriculum)
                .ThenInclude(f => f.Files)
                .Include(u => u.Creator)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                throw new ArgumentException("Invalid course Id");
            }

            return new CourseDetailsViewModel()
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                Image = course.Image,
                Curriculum = course.Curriculum.Select(s => new TopicViewModel()
                {
                    Title = s.Title,
                    Description = s.Description,
                    Videos = s.Videos.Select(v => new VideoViewModel()
                    {
                        Id = v.Id,
                        Title = v.Title,
                        VideoUrl = v.Url,
                    }).ToList(),
                    Files = s.Files.Select(f => new FileViewModel()
                    {
                        Id = f.Id,
                        FileName = f.FileName,
                        ContentType = f.ContentType,
                        FileLength = f.FileLength,
                        SourceId = f.SourceId,
                    }).ToList(),
                }).ToList(),
                Reviews = course.Reviews
                .Where(r => r.IsDeleted == false)
                .Select(r => new ReviewViewModel()
                {
                    Id = r.Id,
                    UserFullName = $"{r.User.FirstName} {r.User.LastName}",
                    UserProfileImage = r.User.ProfileImage,
                    Comment = r.Comment,
                    Rating = r.Rating,
                    DateOfPublication = r.DateOfPublication,
                }).ToList(),
                Goals = course.Goals,
                Requirements = course.Requirements,
                Level = course.Level,
                CategoryId = course.CategoryId,
                SubCategoryId = course.SubCategoryId,
                LanguageId = course.LanguageId,
                Description = course.Description,
                Price = course.Price,
                CreatedOn = course.CreatedOn,
                LastUpdate = course.LastUpdate,
                Creator = new UserViewModel()
                {
                    Id = course.Creator.Id,
                    FirstName = course.Creator.FirstName,
                    LastName = course.Creator.LastName,
                    Email = course.Creator.Email,
                    ProfileImage = course.Creator.ProfileImage,
                },
                Rating = Math.Round(course.Rating),
            };
        }

        public async Task CreateCourse(AddCourseViewModel courseModel, string creatorId)
        {
            for (int i = 0; i < courseModel.Curriculum.Count; i++)
            {
                var videos = await this.videoService.ReplaceVideoUrls(courseModel.Curriculum[i].Videos);
                courseModel.Curriculum[i].Videos = (IList<AddVideoViewModel>)videos;
            }

            var course = new Course
            {
                Title = courseModel.Title,
                Subtitle = courseModel.Subtitle,
                Image = courseModel.Image,
                Goals = courseModel.Goals,
                Requirements = courseModel.Requirements,
                Curriculum = courseModel.Curriculum.Select(c => new Topic()
                {
                    Title = c.Title,
                    Description = c.Description,
                    Videos = c.Videos.Select(v => new Video()
                    {
                        Title = v.Title,
                        Url = v.VideoUrl,
                    }).ToList(),
                    Files = c.FilesInfo.Select(f => new ApplicationFile()
                    {
                        FileName = f.FileName,
                        FileLength = f.FileLength,
                        SourceId = f.SourceId,
                        ContentType = f.ContentType,
                    }).ToList(),
                }).ToList(),
                Level = courseModel.Level,
                CategoryId = courseModel.CategoryId,
                //to do
                SubCategoryId = courseModel.CategoryId,
                LanguageId = courseModel.LanguageId,
                Description = courseModel.Description,
                CreatorId = creatorId,
                Price = courseModel.Price,
                CreatedOn = DateTime.Now,
            };

            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAllNotApproved()
        {
            var allCourses = await this.repository
                .AllReadonly<Course>()
                .Where(c => c.IsDeleted == false && c.IsApproved == false)
                .Select(c => new CourseListingViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Image = c.Image,
                    Price = c.Price,
                }).ToListAsync();

            return allCourses;
        }

        public async Task<Course> GetCourseById(int courseId)
            => await this.repository.GetByIdAsync<Course>(courseId);

        public async Task AddStudentToCourse(int courseId, string studentId)
        {
            var course = await this.repository.AllReadonly<Course>()
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                throw new Exception();
            }

            var student = await this.repository
                .GetByIdAsync<ApplicationUser>(studentId);
            if (student == null)
            {
                throw new Exception();
            }

            var addUserToCourse = new CourseApplicationUser()
            {
                CourseId = courseId,
                StudentId = studentId,
            };

            await this.repository.AddAsync(addUserToCourse);
            await this.repository.SaveChangesAsync();
        }

        public async Task ApproveCourse(int courseId)
        {
            var course = await this.GetCourseById(courseId);
            if (course == null)
            {
                throw new Exception("not exist");
            }

            course.IsApproved = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAll(
            string? category = null,
            string? subCategory = null,
            string? searchTerm = null,
            string? language = null,
            decimal minPrice = 0,
            decimal maxPrice = 2000,
            int currentPage = 1,
            int coursessPerPage = 1,
            CourseSorting sorting = CourseSorting.Newest)
        {
            var courses = this.repository.AllReadonly<Course>()
                .Include(c => c.Reviews)
                .Where(c => c.IsDeleted == false && c.IsApproved == true);

            if (!string.IsNullOrEmpty(category))
            {
                courses = courses.Where(c => c.Category.Title == category);
            }

            if (!string.IsNullOrEmpty(subCategory))
            {
                courses = courses.Where(c => c.SubCategory.Title == subCategory);
            }

            if (!string.IsNullOrEmpty(language))
            {
                courses = courses.Where(c => c.Language.Title == language);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = $"%{searchTerm.ToLower()}%";

                courses = courses.Where(c =>
                EF.Functions.Like(c.Title.ToLower(), searchTerm) ||
                EF.Functions.Like(c.Subtitle.ToLower(), searchTerm) ||
                EF.Functions.Like(c.Description.ToLower(), searchTerm));
            }

            courses = courses.Where(c => c.Price >= minPrice);
            courses = courses.Where(c => c.Price <= maxPrice);

            var result = await courses
                .Skip((currentPage - 1) * coursessPerPage)
                .Take(coursessPerPage)
                .Select(c => new CourseListingViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Image = c.Image,
                    Rating = c.Rating,
                    Price = c.Price,
                    TotalCoursesCount = courses.Count(),
                }).ToListAsync();

            result = sorting switch
            {
                CourseSorting.Price => result
                    .OrderBy(c => c.Price).ToList(),
                CourseSorting.HighestRated => result
                    .OrderBy(c => c.Rating).ToList(),
                _ => result.OrderByDescending(c => c.Id).ToList()
            };

            return result;
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAllEnroledCourses(string userId)
        {
            var user = await this.repository
                .AllReadonly<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(c => c.CoursesEnrolled)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Invalid Id");
            }

            return user.CoursesEnrolled.Select(c => new CourseListingViewModel()
            {
                Id = c.Course.Id,
                Title = c.Course.Title,
                Image = c.Course.Image,
                Price = c.Course.Price,
            });
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAllCreatedCourses(string userId)
        {
            var user = await this.repository
                .AllReadonly<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(c => c.CoursesCreated)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Invalid Id");
            }

            return user.CoursesCreated
                .Where(c => c.IsDeleted == false)
                .Select(c => new CourseListingViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Price = c.Price,
                    Image = c.Image,
                    //todo rating
                });
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAllArchivedCourses(string userId)
        {
            var user = await this.repository
                .AllReadonly<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(c => c.CoursesCreated)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Invalid User");
            }

            return user.CoursesCreated
                .Where(c => c.IsDeleted == true)
                .Select(c => new CourseListingViewModel()
                {
                    Id = c.Id,
                    Title = c.Title,
                    Price = c.Price,
                    Image = c.Image,
                    //todo rating
                });
        }

        public async Task Delete(int courseId, string userId, bool isAdministrator = false)
        {
            var course = await this.GetCourseById(courseId);
            if (course == null)
            {
                throw new Exception("Invalid Course");
            }

            var user = await this.repository.GetByIdAsync<ApplicationUser>(userId);
            if (user == null)
            {
                throw new Exception("Invalid User");
            }

            if (course.CreatorId != user.Id)
            {
                if (isAdministrator == false)
                {
                    throw new Exception("User doesn't have permission to delete this course");
                }
            }

            course.IsDeleted = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> DoUserHavePermission(string userId, int courseId)
        {
            var user = await this.repository
                .AllReadonly<ApplicationUser>()
                .Where(u => u.Id == userId)
                .Include(u => u.CoursesCreated)
                .Include(u => u.CoursesEnrolled)
                .ThenInclude(c => c.Course)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new Exception("Invalid user");
            }

            if (user.CoursesEnrolled.Any(c => c.Course.Id == courseId)
                || user.CoursesCreated.Any(c => c.Id == courseId))
            {
                return true;
            }

            return false;
        }
    }
}
