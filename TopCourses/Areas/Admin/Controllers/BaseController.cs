namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;

    [Authorize(Roles = RoleConstants.Administrator)]
    [Area("Admin")]
    public class BaseController : Controller
    {

    }
}
