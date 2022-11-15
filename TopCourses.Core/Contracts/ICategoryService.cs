namespace TopCourses.Core.Contracts
{
    using System.Collections.Generic;
    using TopCourses.Core.Models.Category;
    using TopCourses.Core.Models.Language;

    public interface ICategoryService
    {
        public Task<IEnumerable<AddCategoryViewModel>> GetAllCategories();

        public Task CreateCategory(AddCategoryViewModel model);

        Task<EditCategoryViewModel> GetCategoryForEdit(int id);

        Task Update(EditCategoryViewModel model);

        Task Delete(int id);
    }
}
