namespace TopCourses.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Review;

    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;
        private readonly ILogger logger;

        public ReviewController(
            IReviewService revielService,
            ILogger<ReviewController> logger)
        {
            this.reviewService = revielService;
            this.logger = logger;
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

            try
            {
                await this.reviewService.AddReview(model);
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "ReviewController/addReview");
            }

            return this.RedirectToAction("Details", "Course", new { id = model.CourseId });
        }
    }
}
