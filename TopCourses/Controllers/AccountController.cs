namespace TopCourses.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.User;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Models;

    public class AccountController : BaseController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IUserService userService;

        public AccountController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this.userService = userService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            var model = new RegisterViewModel();
            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var isUsernameexists = await this.userManager.Users.AnyAsync(u => u.UserName == model.UserName);
            if (isUsernameexists)
            {
                this.ModelState.AddModelError(nameof(model.UserName), "Username already exists");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                EmailConfirmed = true,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await this.signInManager.SignInAsync(user, isPersistent: false);
                return this.RedirectToAction("Index", "Home");
            }

            foreach (var item in result.Errors)
            {
                this.ModelState.AddModelError(string.Empty, item.Description);
            }

            return this.View(model);
        }

        [AllowAnonymous]
        public IActionResult Login(string? returnUrl = null)
        {
            var model = new LoginViewModel()
            {
                ReturnUrl = returnUrl,
            };
            model.ReturnUrl = returnUrl ?? this.Url.Content("~/");
            this.ViewData["ReturnUrl"] = model.ReturnUrl;

            return this.View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await this.signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (model.ReturnUrl != null)
                    {
                        return this.Redirect(model.ReturnUrl);
                    }

                    this.TempData[MessageConstant.SuccessMessage] = "Successfully logged in";
                    return this.RedirectToAction("Index", "Home");
                }
            }

            this.ModelState.AddModelError(string.Empty, "Invalid Login");
            return this.View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await this.signInManager.SignOutAsync();
            return this.RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> MyProfile()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await this.userService.GetUserProfile(userId);
            return this.View(model);
        }

        public async Task<IActionResult> Edit()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var model = await this.userService.GetUserForEdit(userId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.Id = userId;
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.userService.UpdateUser(model);
            return this.RedirectToAction("MyProfile", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string? returnurl = null)
        {
            returnurl = returnurl ?? this.Url.Content("~/");
            if (this.ModelState.IsValid)
            {
                //get the info about the user from external login provider
                var info = await this.signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return this.View("Error");
                }

                var user = new ApplicationUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                };

                var result = await this.userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await this.userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await this.signInManager.SignInAsync(user, isPersistent: false);
                        await this.signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return this.LocalRedirect(returnurl);
                    }
                }

                this.ModelState.AddModelError("Email", "Error occuresd");
            }

            this.ViewData["ReturnUrl"] = returnurl;
            return this.View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnurl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return this.View(nameof(this.Login));
            }

            var info = await this.signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return this.RedirectToAction(nameof(this.Login));
            }

            //Sign in the user with this external login provider, if the user already has a login.
            var result = await this.signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
            if (result.Succeeded)
            {
                //update any authentication tokens
                await this.signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return this.LocalRedirect(returnurl);
            }
            else
            {
                //If the user does not have account, then we will ask the user to create an account.
                this.ViewData["ReturnUrl"] = returnurl;
                this.ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                return this.View("ExternalLoginConfirmation", new ExternalLoginViewModel { Email = email });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult ExternalLogin(string provider, string returnurl = null)
        {
            returnurl = returnurl ?? this.Url.Content("~/");
            //request a redirect to the external login provider
            var redirecturl = this.Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnurl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirecturl);
            return this.Challenge(properties, provider);
        }

        public async Task<IActionResult> CreateRoles()
        {
            await this.roleManager.CreateAsync(new IdentityRole(RoleConstants.Administrator));
            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string userId = "bf5a260a-7446-418e-bf8e-29f8608906bb";
            var user = await this.userManager.FindByIdAsync(userId);
            await this.userManager.AddToRolesAsync(user, new string[] { RoleConstants.Administrator });
            return this.RedirectToAction("Index", "Home");
        }
    }
}
