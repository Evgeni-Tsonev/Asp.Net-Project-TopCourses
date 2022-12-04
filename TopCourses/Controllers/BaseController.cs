namespace TopCourses.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;

    [Authorize]
    public class BaseController : Controller
    {
        public string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
