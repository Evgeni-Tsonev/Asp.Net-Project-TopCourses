namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Core.Models.Review;
    using TopCourses.Core.Models.Video;

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
                ImageUrl = course.ImageUrl,
                Curriculum = course.Curriculum.Select(s => new TopicViewModel()
                {
                    Title = s.Title,
                    Description = s.Description,
                    //Video = new VideoModel 
                    //{
                    //    VideoUrl = s?.VideoUrl
                    //}
                }).ToList(),
                Reviews = course.Reviews.Select(r => new ReviewViewModel()
                {
                    UserFullName = $"{r.User.FirstName} {r.User.LastName}",
                    Comment = r.Comment,
                    Rating = r.Rating,
                    DateOfPublication = r.DateOfPublication
                }).ToList(),
                Level = course.Level,
                CategoryId = course.CategoryId,
                LanguageId = course.LanguageId,
                Description = course.Description,
                Price = course.Price
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
                ImageUrl = courseModel.ImageUrl,
                Goals = courseModel.Goals,
                Requirements = courseModel.Requirements,
                Curriculum = courseModel.Curriculum.Select(c => new Topic()
                {
                    Title = c.Title,
                    Description = c.Description,
                    Videos = c.Videos.Select(v => new Video()
                    {
                        Title = v.Title,
                        Url = v.VideoUrl
                    }).ToList()
                }).ToList(),
                Level = courseModel.Level,
                CategoryId = courseModel.CategoryId,
                LanguageId = courseModel.LanguageId,
                Description = courseModel.Description,
                CreatorId = creatorId,
                Price = courseModel.Price,
            };

            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseListingViewModel>> GetAll()
        {
            var allCourses = await this.repository.AllReadonly<Course>()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CourseListingViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price
                }).ToListAsync();

            return allCourses;
        }

        public async Task<Course> GetCourseById(int courseId)
            => await this.repository.GetByIdAsync<Course>(courseId);

        public async Task AddStudentToCourse(int courseId, string studentId)
        {
            var course = await this.repository
                .AllReadonly<Course>()
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

            course.Students.Add(new CourseApplicationUser()
            {
                CourseId = courseId,
                StudentId = studentId
            });

            await this.repository.SaveChangesAsync();
        }
    }
}
