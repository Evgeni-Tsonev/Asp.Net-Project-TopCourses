namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;

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
            return View(allCategories);
        }

        public IActionResult AddCategory()
        {
            var category = new CategoryModel();
            return View(category);
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoriesService.CreateCategory(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddSubCategory()
        {
            var category = new CategoryModel();
            var mainCategories = await categoriesService.GetAllMainCategories();
            ViewBag.mainCategories = mainCategories;

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(CategoryModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoriesService.CreateSubCategory(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
