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

            ViewData["Title"] = "Categories";

            ViewData["SubCategoriesTitle"] = "Sub categories";

            return View(allCategories);
        }

        public IActionResult AddCategory()
        {
            var category = new AddCategoryViewModel();

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoriesService.CreateCategory(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddSubCategory([FromRoute] int id)
        {
            var category = new AddCategoryViewModel();

            category.ParentId = id;

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategoryPost(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddSubCategory", model);
            }

            await this.categoriesService.CreateCategory(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.categoriesService.GetCategoryForEdit(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", model);
            }

            await this.categoriesService.Update(model);

            return RedirectToAction("Index", "Category");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            await this.categoriesService.Delete(id);

            return RedirectToAction("Index", "Category");
        }
    }
}
