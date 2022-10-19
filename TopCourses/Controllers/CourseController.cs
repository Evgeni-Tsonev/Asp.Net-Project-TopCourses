namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Identity;


    public class CourseController : Controller
    {
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;
        private readonly UserManager<ApplicationUser> userManager;

        public CourseController(ICourseService courseService,
                                ICategoryService categoryService,
                                ILanguageService languageService,
                                UserManager<ApplicationUser> userManager)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.languageService = languageService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await this.courseService.GetAll();
            return View(allCourses);
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();

            var course = new AddCourseModel()
            {
                Categories = categories,
                Languages = languages,
            };

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseModel model)
        {
            var categories = await this.categoryService.GetAllMainCategories();
            if (!categories.Any(b => b.Id == model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            var languages = await this.languageService.GetAll();
            if (!languages.Any(b => b.Id == model.LanguageId))
            {
                this.ModelState.AddModelError(nameof(model.LanguageId), "Language does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Languages = languages;
                model.Categories = categories;
                return View(model);
            }

            var currentUserId = GetUserId();
            await this.courseService.CreateCourse(model, currentUserId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var details = await this.courseService.GetCourseDetails(id);
            return View(details);
        }

        public IActionResult Video()
        {
            return View();
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
