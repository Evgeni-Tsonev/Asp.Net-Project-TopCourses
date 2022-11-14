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
            var category = new CategoryViewModel();

            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoriesService.CreateCategory(model);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AddSubCategory([FromRoute]int id)
        {
            var category = new CategoryViewModel();

            category.ParentId = id;

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategoryPost(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("AddSubCategory", model);
            }

            await categoriesService.CreateCategory(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
