namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models.Category;

    public interface ICategoryService
    {
        public Task<IEnumerable<CategoryViewModel>> GetAllMainCategories();

        public Task<IEnumerable<CategoryViewModel>> GetAllSubCategories(int mainCategoryId);

        public Task CreateCategory(CategoryViewModel model);

        Task<EditCategoryViewModel> GetCategoryForEdit(int id);

        Task Update(EditCategoryViewModel model);

        Task Delete(int id);
    }
}
