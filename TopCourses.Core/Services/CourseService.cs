namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Course;
    using System.Text.RegularExpressions;
    using TopCourses.Core.Models.Section;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Core.Models.Review;

    public class CourseService : ICourseService
    {
        private readonly IDbRepository repository;

        public CourseService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CourseDetailsModel> GetCourseDetails(int courseId)
        {
            var course = await this.repository
                .AllReadonly<Course>()
                .Include(c => c.Curriculum)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (course == null)
            {
                throw new ArgumentException("Invalid course Id");
            }

            return new CourseDetailsModel()
            {
                Id = course.Id,
                Title = course.Title,
                Subtitle = course.Subtitle,
                ImageUrl = course.ImageUrl,
                Curriculum = course.Curriculum.Select(s => new SectionModel()
                {
                    Title = s.Title,
                    Description = s.Description,
                    VideoUrl = s?.VideoUrl
                    //Video = new VideoModel 
                    //{
                    //    VideoUrl = s?.VideoUrl
                    //}
                }).ToList(),
                Level = course.Level,
                CategoryId = course.CategoryId,
                LanguageId = course.LanguageId,
                Description = course.Description,
                Price = course.Price
            };
        }

        public async Task CreateCourse(AddCourseModel courseModel, string creatorId)
        {
            foreach (var section in courseModel.Curriculum.Where(s => s.VideoUrl != null))
            {
                var pattern = @"http(?:s)?:\/\/(?:m.)?(?:www\.)?youtu(?:\.be\/|(?:be-nocookie|be)\.com\/(?:watch|[\w]+\?(?:feature=[\w]+.[\w]+\&)?v=|v\/|e\/|embed\/|user\/(?:[\w#]+\/)+))([^&#?\n]+)";

                var videoIdGroup = 1;

                var regex = new Regex(pattern);
                var match = regex.Match(section.VideoUrl);

                if (match.Success)
                {
                    var videoId = match.Groups[videoIdGroup];
                    var processedVideoUrl = $"https://www.youtube.com/embed/{videoId}?autoplay=1";
                    section.VideoUrl = processedVideoUrl;
                }
                else
                {
                    throw new InvalidOperationException("Failed to match VideoUrl Id");
                }
            }

            var course = new Course
            {
                Title = courseModel.Title,
                Subtitle = courseModel.Subtitle,
                ImageUrl = courseModel.ImageUrl,
                Goals = courseModel.Goals,
                Requirements = courseModel.Requirements,
                Curriculum = courseModel.Curriculum.Select(c => new Section()
                {
                    Title = c.Title,
                    Description = c.Description,
                    VideoUrl = c?.VideoUrl
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

        public async Task<IEnumerable<CourseListingModel>> GetAll()
        {
            var allCourses = await this.repository.AllReadonly<Course>()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CourseListingModel
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

        public async Task AddReview(AddReviewViewModel model)
        {
            var user = await this.repository.GetByIdAsync<ApplicationUser>(model.UserId);

            if (user == null)
            {
                throw new Exception();
            }

            var course = await this.repository.GetByIdAsync<Course>(model.CourseId);

            if (course == null)
            {
                throw new Exception();
            }

            var review = new Review()
            {
                Comment = model.Comment,
                Rating = model.Rating,
                CourseId = model.CourseId,
                UserId = model.UserId
            };

            await this.repository.AddAsync(review);
            await this.repository.SaveChangesAsync();
        }
    }
}
