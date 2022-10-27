namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
