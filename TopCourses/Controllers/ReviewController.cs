namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Review;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService revielService)
        {
            this.reviewService = revielService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReview(int id)
        {
            var userId = GetUserId();

            var model = new AddReviewViewModel()
            {
                UserId = userId,
                CourseId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            model.DateOfPublication = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View("CreateReview", model);
            }

            await this.reviewService.AddReview(model);

            return RedirectToAction("Details", "Course", new { id = model.CourseId });
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
