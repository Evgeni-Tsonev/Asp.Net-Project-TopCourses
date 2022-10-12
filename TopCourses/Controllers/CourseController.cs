namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
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
            var user = await this.userManager.GetUserAsync(this.User);
            var course = new AddCourseModel()
            {
                Categories = await this.categoryService.GetAllMainCategories(),
                Languages = await this.languageService.GetAll(),
                CreatorId = user.Id
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

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var details = await this.courseService.GetCourseDetails(id);
            return View(details);
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}
