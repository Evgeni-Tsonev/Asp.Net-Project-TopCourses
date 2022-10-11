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
            return await this.repository.AllReadonly<Course>()
                .Where(c => c.Id == courseId)
                .Select(c => new CourseDetailsModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Subtitle = c.Subtitle,
                    ImageUrl = c.ImageUrl,
                    Requirements = c.Requirements,
                    Goals = c.Goals,
                    Curriculum = c.Curriculum,
                    Level = c.Level,
                    CategoryId = c.CategoryId,
                    LanguageId = c.LanguageId,
                    Description = c.Description,
                    Price = c.Price
                }).FirstOrDefaultAsync();
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
