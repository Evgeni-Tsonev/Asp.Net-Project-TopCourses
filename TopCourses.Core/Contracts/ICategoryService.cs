namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models.Category;

    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> GetAllCategories();

        public Task CreateCategory(CategoryViewModel model);
    }
}
