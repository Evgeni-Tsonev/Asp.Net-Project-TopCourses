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
