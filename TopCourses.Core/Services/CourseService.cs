namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;
    using TopCourses.Infrastructure.Data.Models;
    using WebShopDemo.Core.Data.Common;

    public class CourseService : ICourseService
    {
        private readonly IRepository repository;

        public CourseService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateCourse(CourseModel courseModel)
        {
            var course = new Course
            {
                Title = courseModel.Title,
                Subtitle = courseModel.Subtitle,
                ImageUrl = courseModel.ImageUrl,
                Requirements = courseModel.Requirements,
                Goals = courseModel.Goals,
                Topics = courseModel.Topics,
                Curriculum = courseModel.Curriculum,
                Level = courseModel.Level,
                CategoryId = courseModel.CategoryId,
                LanguageId = courseModel.LanguageId,
                Description = courseModel.Description,
                Price = courseModel.Price,
            };

            await this.repository.AddAsync(course);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseModel>> GetAll()
        {
            return await this.repository.AllReadonly<Course>()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CourseModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Subtitle = c.Subtitle,
                    ImageUrl = c.ImageUrl,
                    Requirements = c.Requirements,
                    Goals = c.Goals,
                    Topics = c.Topics,
                    Curriculum = c.Curriculum,
                    Level = c.Level,
                    CategoryId = c.CategoryId,
                    LanguageId = c.LanguageId,
                    Description = c.Description,
                    Price = c.Price
                }).ToListAsync();
        }
    }
}
