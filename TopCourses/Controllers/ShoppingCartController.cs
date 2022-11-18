namespace TopCourses.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Mvc;
    using Stripe;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Order;

    public class ShoppingCartController : BaseController
    {
        private readonly IShoppingCartService shoppingCartService;
        private readonly IOrderService orderService;
        private readonly ICourseService courseService;

        public ShoppingCartController(IShoppingCartService shoppingCartService, IOrderService orderService, ICourseService courseService)
        {
            this.shoppingCartService = shoppingCartService;
            this.orderService = orderService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var shoppingCart = await this.shoppingCartService.GetShoppingCart(userId);

            return View(shoppingCart);
        }

        public async Task<IActionResult> Add([FromRoute] int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            try
            {
                await this.shoppingCartService.AddCourseInShoppingCart(id, userId);
            }
            catch (Exception ex)
            {
                TempData[MessageConstant.ErrorMessage] = ex.Message;
                return RedirectToAction("Index", "Course");
            }

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

        public async Task<IActionResult> Summary()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var shoppingCart = await this.shoppingCartService.GetShoppingCart(userId);

            return View(shoppingCart);
        }

        [HttpPost]
        public async Task<IActionResult> Summary(string stripeToken)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var shoppingCart = await this.shoppingCartService.GetShoppingCart(userId);

            var order = new OrderViewModel()
            {
                Courses = shoppingCart.Courses,
                TotalPrice = shoppingCart.TotalPrice,
                OrderDate = DateTime.Now,
                OrderStatus = "pending",
                PaymentStatus = "pending"
            };

            order.Id = await this.orderService.AddOrder(order, userId);

            var options = new ChargeCreateOptions
            {
                Amount = Convert.ToInt32(order.TotalPrice * 100),
                Currency = "bgn",
                Description = "Order ID : " + order.Id,
                Source = stripeToken
            };

            var service = new ChargeService();
            Charge charge = service.Create(options);

            if (charge.Id == null)
            {
                order.PaymentStatus = "rejected";
            }
            else
            {
                order.TransactionId = charge.Id;
            }

            if (charge.Status.ToLower() == "succeeded")
            {
                order.PaymentStatus = "approved";
                order.OrderStatus = "approved";
                order.OrderDate = DateTime.Now;
            }

            await this.orderService.UpdateOrder(order);

            foreach (var course in shoppingCart.Courses)
            {
                await this.courseService.AddStudentToCourse(course.Id, userId);
            }

            await this.shoppingCartService.DeleteAllCoursesFromShoppingCart(userId);

            return RedirectToAction("OrderConfirmation", "ShoppingCart", new { id = order.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
