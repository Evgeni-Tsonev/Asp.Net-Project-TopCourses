namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;

    public class CourseController : BaseController
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var allCourses = await this.courseService.GetAllNotApproved();

            return this.View(allCourses);
        }

        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var course = await this.courseService.GetCourseDetails(id);

            this.ViewData["Title"] = $"{course.Title}";

            this.ViewData["Subtitle"] = $"{course.Subtitle}";

            return this.View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            await this.courseService.ApproveCourse(id);

            this.TempData[MessageConstant.SuccessMessage] = "Course approved successfully";

            return this.RedirectToAction("Index", "Course", new { area = "admin" });
        }
    }
}
