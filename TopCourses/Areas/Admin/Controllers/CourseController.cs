namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
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

            //var url = course.Curriculum.Select(u => u.VideoUrl).FirstOrDefault();

            this.ViewData["Title"] = $"{course.Title}";

            this.ViewData["Subtitle"] = $"{course.Subtitle}";

            //TempData["VideoUrl"] = url;

            return this.View(course);
        }
    }
}
