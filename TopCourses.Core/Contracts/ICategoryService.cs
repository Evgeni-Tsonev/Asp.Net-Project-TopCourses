namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models;

    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryModel>> GetAllCategories();
        public Task CreateCategory(CategoryModel model);
        public Task CreateSubCategory(CategoryModel model);
    }
}
