namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;
    using MongoDB.Driver.GridFS;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;

    public class CourseController : BaseController
    {
        private readonly ICourseService courseService;
        private readonly GridFSBucket bucket;
        private readonly ILogger logger;

        public CourseController(
            ICourseService courseService,
            IBucket bucketContex,
            ILogger<CourseController> logger)
        {
            this.courseService = courseService;
            this.bucket = bucketContex.Create();
            this.logger = logger;
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
            try
            {
                await this.courseService.ApproveCourse(id);
                this.TempData[MessageConstant.SuccessMessage] = "Course approved successfully";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CourseController/Approve");
            }

            return this.RedirectToAction("Index", "Course", new { area = "admin" });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int courseId)
        {
            var userId = this.GetUserId();
            var isAdministrator = this.User.IsInRole(RoleConstants.Administrator);

            try
            {
                await this.courseService.Delete(courseId, userId, isAdministrator);
                this.TempData[MessageConstant.SuccessMessage] = "Course deleted successfully";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "CourseController/Approve");
            }

            return this.RedirectToAction("Index", "Course", new { area = "admin" });
        }

        public async Task<IActionResult> Download(string id)
        {
            var stream = await this.bucket.OpenDownloadStreamAsync(new ObjectId(id));
            var fileName = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "FileName");
            var fileType = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "Type");
            return this.File(stream, fileType.Value.ToString(), fileName.Value.ToString());
        }
    }
}