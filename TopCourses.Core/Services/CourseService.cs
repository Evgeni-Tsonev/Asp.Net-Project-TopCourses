namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Course;

    public class CourseService : ICourseService
    {
        private readonly IRepository repository;

        public CourseService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task<CourseDetailsModel> GetCourseDetails(int courseId)
        {
            var course = await this.repository.AllReadonly<Course>().FirstOrDefaultAsync(x => x.Id == courseId);

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
                Requirements = course.Requirements,
                Goals = course.Goals,
                Curriculum = course.Curriculum,
                Level = course.Level,
                CategoryId = course.CategoryId,
                LanguageId = course.LanguageId,
                Description = course.Description,
                Price = course.Price
            };
        }

        public async Task CreateCourse(AddCourseModel courseModel)
        {
            var course = new Course
            {
                Title = courseModel.Title,
                Subtitle = courseModel.Subtitle,
                ImageUrl = courseModel.ImageUrl,
                Requirements = courseModel.Requirements,
                Goals = courseModel.Goals,
                Curriculum = courseModel.Curriculum,
                Level = courseModel.Level,
                CategoryId = courseModel.CategoryId,
                LanguageId = courseModel.LanguageId,
                Description = courseModel.Description,
                CreatorId = courseModel.CreatorId,
                Price = courseModel.Price,
            };

            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseListingModel>> GetAll()
        {
            return await this.repository.AllReadonly<Course>()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CourseListingModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ImageUrl = c.ImageUrl,
                    Price = c.Price
                }).ToListAsync();
        }
    }
}
