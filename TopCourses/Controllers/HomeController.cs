namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using TopCourses.Core.Contracts;
    using TopCourses.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICourseService courseService;
        public HomeController(ILogger<HomeController> logger,
            ICourseService courseService)
        {
            _logger = logger;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await courseService.GetAll();
            return View(allCourses);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}