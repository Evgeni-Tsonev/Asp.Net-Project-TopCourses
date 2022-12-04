namespace TopCourses.Controllers
{
    using System.Security.Claims;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
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
            return this.View();
        }

        public IActionResult CreateReview(int id)
        {
            var userId = this.GetUserId();
            var model = new AddReviewViewModel()
            {
                UserId = userId,
                CourseId = id,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(AddReviewViewModel model)
        {
            var saitizer = new HtmlSanitizer();
            model.Comment = saitizer.Sanitize(model.Comment);
            model.DateOfPublication = DateTime.Now;
            if (!this.ModelState.IsValid)
            {
                return this.View("CreateReview", model);
            }

            await this.reviewService.AddReview(model);
            return this.RedirectToAction("Details", "Course", new { id = model.CourseId });
        }
    }
}
