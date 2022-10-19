namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Models;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            ICourseService courseService)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}