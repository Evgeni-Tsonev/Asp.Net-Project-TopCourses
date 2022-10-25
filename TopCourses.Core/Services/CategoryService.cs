namespace TopCourses.Core.Services
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Core.Data.Common;

    public class CategoryService : ICategoryService
    {
        private readonly IDbRepository repository;

        public CategoryService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateCategory(CategoryModel model)
        {
            var category = new Category
            {
                Title = model.Title
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();
        }

        public async Task CreateSubCategory(CategoryModel model)
        {
            var subCategory = new Category
            {
                Title = model.Title,
                ParentId = model.ParentId
            };

            await this.repository.AddAsync(subCategory);
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryModel>> GetAllMainCategories()
        {
            var categories = await this.repository.AllReadonly<Category>()
                .Where(c => c.IsDeleted == false && c.Parent == null)
                .Select(c => new CategoryModel
                {
                    Id = c.Id,
                    Title = c.Title
                }).ToListAsync();

            return categories;
        }
    }
}
