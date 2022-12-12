namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllMainCategories();

        Task<IEnumerable<CategoryViewModel>> GetAllSubCategories(int mainCategoryId);

        Task CreateCategory(CategoryViewModel model);

        Task<EditCategoryViewModel> GetCategoryForEdit(int id);

        Task Update(EditCategoryViewModel model);

        Task Delete(int id);

        Task<CategoryViewModel> GetCategoryById(int id);
    }
}
