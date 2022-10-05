namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;
    using TopCourses.Core.Services;

    public class CourseController : Controller
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await courseService.GetAll();
            return View(allCourses);
        }

        public IActionResult Add()
        {
            var course = new CourseModel();
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseModel model)
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
