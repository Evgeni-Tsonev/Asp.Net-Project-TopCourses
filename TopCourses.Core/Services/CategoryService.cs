namespace TopCourses.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Data.Common;
    using TopCourses.Core.Models.Category;
    using TopCourses.Infrastructure.Data.Models;

    public class CategoryService : ICategoryService
    {
        private readonly IDbRepository repository;

        public CategoryService(IDbRepository repository)
        {
            this.repository = repository;
        }

        public async Task CreateCategory(CategoryViewModel model)
        {
            var category = new Category
            {
                Title = model.Title,
                ParentId = model?.ParentId,
            };

            await this.repository.AddAsync(category);
            await this.repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await this.repository.GetByIdAsync<Category>(id);
            if (category == null)
            {
                throw new Exception();
            }

            category.IsDeleted = true;
            await this.repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var categories = await this.repository.AllReadonly<Category>()
                .Where(c => c.IsDeleted == false)
                .Select(c => new CategoryViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    ParentId = c.ParentId,
                }).ToListAsync();

            var categoriesToReturn = new List<CategoryViewModel>();
            foreach (var mainCategory in categories.Where(c => c.ParentId == null))
            {
                foreach (var subCategory in categories.Where(c => c.ParentId == mainCategory.Id))
                {
                    mainCategory.SubCategories.Add(subCategory);
                }

                categoriesToReturn.Add(mainCategory);
            }

            return categoriesToReturn;
        }

        public async Task<EditCategoryViewModel> GetCategoryForEdit(int id)
        {
            var model = await this.repository.GetByIdAsync<Category>(id);
            if (model == null)
            {
                throw new Exception();
            }

            return new EditCategoryViewModel()
            {
                Id = model.Id,
                Title = model.Title,
            };
        }

        public async Task Update(EditCategoryViewModel model)
        {
            var category = await this.repository.GetByIdAsync<Category>(model.Id);
            if (category == null)
            {
                throw new Exception();
            }

            category.Title = model.Title;
            await this.repository.SaveChangesAsync();
        }
    }
}
