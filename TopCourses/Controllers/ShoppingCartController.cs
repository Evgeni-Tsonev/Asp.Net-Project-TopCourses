namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;

    public class ShoppingCartController : Controller
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var courses = await this.shoppingCartService.GetAllShoppingCartCoursess(userId);
            return View(courses);
        }

        public async Task<IActionResult> Add([FromRoute]int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await this.shoppingCartService.AddCourseInShoppingCart(id, userId);

            TempData[MessageConstant.SuccessMessage] = "Successfully added course to Shopping cart";

            return RedirectToAction("Index", "Course");
        }
    }
}
