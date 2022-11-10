namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Infrastructure.Data.Identity;

    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string email1 = "evgeni@abv.bg";

            var user = await userManager.FindByEmailAsync(email1);

            await userManager.AddToRoleAsync(user, RoleConstants.Administrator);

            return RedirectToAction("Index", "Home");
        }
    }
}
