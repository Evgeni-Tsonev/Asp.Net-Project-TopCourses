namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;
    using TopCourses.Core.Services;

    public class CategoryController : Controller
    {
        private readonly ICategoryService categoriesService;

        public CategoryController(ICategoryService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> Index()
        {
            var allCategories = await this.categoriesService.GetAllCategories();
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
    }
}
