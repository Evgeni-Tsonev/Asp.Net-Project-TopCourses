namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;

    public class VideoController : BaseController
    {
        private readonly IVideoService videoService;
        private readonly ICourseService courseService;

        public VideoController(IVideoService videoService, ICourseService courseService)
        {
            this.videoService = videoService;
            this.courseService = courseService;
        }

        public async Task<IActionResult> Video(int videoId, int courseId)
        {
            var userId = this.GetUserId();
            var doUserHavePermission = await this.courseService.DoUserHavePermission(userId, courseId);
            if (!doUserHavePermission)
            {
                this.TempData[MessageConstant.WarningMessage] = "You do not have access to this resource.";
                return this.RedirectToAction("Details", "Course", new { Id = courseId });
            }

            var model = await this.videoService.GetVideoById(videoId);
            return this.View(model);
        }
    }
}
