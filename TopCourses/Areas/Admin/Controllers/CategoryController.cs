namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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
            var allCategories = await this.categoriesService.GetAllCategories();
            this.ViewData["Title"] = "Categories";
            this.ViewData["SubCategoriesTitle"] = "Sub categories";
            return this.View(allCategories);
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

        public IActionResult AddSubCategory([FromRoute] int id)
        {
            var category = new CategoryViewModel();
            category.ParentId = id;
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
