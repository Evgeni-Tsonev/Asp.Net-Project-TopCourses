namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Areas.Admin.Models;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Category;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoriesService;

        public CategoryController(ICategoryService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var allCategories = await this.categoriesService.GetAllMainCategories();
            this.ViewData["Title"] = "Categories";
            return this.View(allCategories);
        }

        public async Task<IActionResult> SubCategories(int id)
        {
            var model = new SubCategoriesListModel();
            model.MainCategoryId = id;
            model.SubCategories = await this.categoriesService.GetAllSubCategories(id);
            this.ViewData["Title"] = "Sub Categories";
            return this.View(model);
        }

        public IActionResult AddCategory()
        {
            var category = new CategoryViewModel();
            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.categoriesService.CreateCategory(model);
            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult AddSubCategory(int parentId)
        {
            var category = new CategoryViewModel();
            category.ParentId = parentId;
            return this.View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategoryPost(CategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("AddSubCategory", model);
            }

            await this.categoriesService.CreateCategory(model);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.categoriesService.GetCategoryForEdit(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditCategoryViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View("Edit", model);
            }

            await this.categoriesService.Update(model);
            return this.RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await this.categoriesService.Delete(id);
            return this.RedirectToAction("Index", "Category");
        }
    }
}
