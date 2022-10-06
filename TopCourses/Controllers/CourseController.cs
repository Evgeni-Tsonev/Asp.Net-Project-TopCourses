namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;

    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;

        public CourseController(ICourseService courseService, 
                                ICategoryService categoryService, 
                                ILanguageService languageService)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.languageService = languageService;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await courseService.GetAll();
            return View(allCourses);
        }

        public async Task<IActionResult> Add()
        {
            var course = new AddCourseModel()
            {
                Categories = await this.categoryService.GetAllCategories(),
                Languages = await this.languageService.GetAll()
            };
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.courseService.CreateCourse(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
