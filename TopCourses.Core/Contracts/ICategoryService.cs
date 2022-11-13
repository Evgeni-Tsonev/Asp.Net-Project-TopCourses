namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models.Category;

    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryModel>> GetAllMainCategories();
        public Task CreateCategory(CategoryModel model);
        public Task CreateSubCategory(CategoryModel model);
    }
}
