namespace TopCourses.Areas.Admin.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Areas.Admin.Models;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Category;

    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoriesService;
        private readonly ILogger logger;

        public CategoryController(
            ICategoryService categoriesService,
            ILogger<CategoryController> logger)
        {
            this.categoriesService = categoriesService;
            this.logger = logger;
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
            var sanitizer = new HtmlSanitizer();
            model.Title = sanitizer.Sanitize(model.Title);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.categoriesService.CreateCategory(model);
                this.TempData[MessageConstant.SuccessMessage] = "Successfully created category";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CategoryController/AddCategory");
            }

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
            var sanitizer = new HtmlSanitizer();
            model.Title = sanitizer.Sanitize(model.Title);
            if (!this.ModelState.IsValid)
            {
                return this.View("AddSubCategory", model);
            }

            try
            {
                await this.categoriesService.CreateCategory(model);
                this.TempData[MessageConstant.SuccessMessage] = "Successfully created subcategory";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CategoryController/AddSubCategoryPost");
            }

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
            var sanitizer = new HtmlSanitizer();
            model.Title = sanitizer.Sanitize(model.Title);
            if (!this.ModelState.IsValid)
            {
                return this.View("Edit", model);
            }

            try
            {
                await this.categoriesService.Update(model);
                this.TempData[MessageConstant.SuccessMessage] = "Successfully updated";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CategoryController/Update");
            }

            return this.RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            try
            {
                await this.categoriesService.Delete(id);
                this.TempData[MessageConstant.SuccessMessage] = "Successfully deleted";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CategoryController/Delete");
            }

            return this.RedirectToAction("Index", "Category");
        }
    }
}
