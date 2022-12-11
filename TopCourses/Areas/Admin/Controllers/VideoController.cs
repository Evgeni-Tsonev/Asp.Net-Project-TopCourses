namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;

    public class VideoController : BaseController
    {
        private readonly IVideoService videoService;

        public VideoController(IVideoService videoService)
        {
            this.videoService = videoService;
        }

        public async Task<IActionResult> Video(int id)
        {
            var model = await this.videoService.GetVideoById(id);

            return this.View(model);
        }
    }
}
