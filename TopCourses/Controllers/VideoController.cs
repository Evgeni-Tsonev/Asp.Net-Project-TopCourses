namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Video;

    public class VideoController : BaseController
    {
        private readonly IVideoService videoService;
        private readonly ICourseService courseService;
        private readonly ILogger logger;

        public VideoController(
            IVideoService videoService,
            ICourseService courseService,
            ILogger<VideoController> logger)
        {
            this.videoService = videoService;
            this.courseService = courseService;
            this.logger = logger;
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

            var model = new VideoViewModel();
            try
            {
                model = await this.videoService.GetVideoById(videoId);
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CourseController/Approve");
            }

            return this.View(model);
        }
    }
}
