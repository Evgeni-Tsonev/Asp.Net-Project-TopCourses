namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.User;
    using TopCourses.Infrastructure.Data.Identity;

    public class UserController : BaseController
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUserService userService;
        private readonly ILogger logger;

        public UserController(
                              UserManager<ApplicationUser> userManager,
                              IUserService userService,
                              RoleManager<IdentityRole> roleManager,
                              ILogger<UserController> logger)
        {
            this.userManager = userManager;
            this.userService = userService;
            this.roleManager = roleManager;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = await this.userService.GetUsers();

            return this.View(model);
        }

        public async Task<IActionResult> Roles(string id)
        {
            var user = await this.userService.GetUserById(id);
            var model = new UserRolesViewModel()
            {
                UserId = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
            };

            this.ViewBag.RoleItems = this.roleManager.Roles
                .ToList()
                .Select(r => new SelectListItem()
                {
                    Text = r.Name,
                    Value = r.Name,
                    Selected = this.userManager.IsInRoleAsync(user, r.Name).Result,
                }).ToList();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Roles(UserRolesViewModel model)
        {
            var user = await this.userService.GetUserById(model.UserId);
            var userRoles = await this.userManager.GetRolesAsync(user);
            await this.userManager.RemoveFromRolesAsync(user, userRoles);
            if (model.RoleNames?.Length > 0)
            {
                try
                {
                    await this.userManager.AddToRolesAsync(user, model.RoleNames);
                    this.TempData[MessageConstant.SuccessMessage] = "Syccsessfully added role to user";
                }
                catch (Exception ex)
                {
                    this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                    this.logger.LogError(ex, "UserController/Roles");
                }
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
