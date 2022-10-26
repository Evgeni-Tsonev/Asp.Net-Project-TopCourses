namespace TopCourses.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;

    public class ShoppingCartController : BaseController
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

        public async Task<IActionResult> Add([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            await this.shoppingCartService.AddCourseInShoppingCart(id, userId);

            TempData[MessageConstant.SuccessMessage] = "Successfully added course to Shopping cart";

            return RedirectToAction("Index", "Course");
        }

        public async Task<IActionResult> Delete(int id)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await this.shoppingCartService.DeleteCourseFromShoppingCart(id, userId);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteAll()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await this.shoppingCartService.DeleteAllCoursesFromShoppingCart(userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
