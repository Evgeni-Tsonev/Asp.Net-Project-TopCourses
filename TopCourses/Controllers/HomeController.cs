namespace TopCourses.Controllers
{
    using System.Diagnostics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Models;

    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly ICourseService courseService;

        public HomeController(
            ILogger<HomeController> logger,
            ICourseService courseService)
        {
            this.logger = logger;
            this.courseService = courseService;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var courses = await this.courseService.GetRandomCourses();

            return this.View(courses);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}