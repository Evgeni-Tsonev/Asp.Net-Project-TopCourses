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

        public CourseController(
            ICourseService courseService,
            IBucket bucketContex)
        {
            this.courseService = courseService;
            this.bucket = bucketContex.Create();
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

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int courseId)
        {
            var userId = this.GetUserId();
            var isAdministrator = this.User.IsInRole(RoleConstants.Administrator);
            await this.courseService.Delete(courseId, userId, isAdministrator);
            return this.RedirectToAction("MyLearning");
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