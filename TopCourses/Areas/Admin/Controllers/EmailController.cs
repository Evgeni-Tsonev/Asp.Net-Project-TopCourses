namespace TopCourses.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Services.Messaging;

    public class EmailController : BaseController
    {
        private readonly ICourseService courseService;
        private readonly IEmailSender emailService;

        public EmailController(ICourseService courseService, IEmailSender emailService)
        {
            this.courseService = courseService;
            this.emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var course = await this.courseService.GetCourseById(id);
            var mailContent = "Your Course is published Succsessfully";
            await this.emailService.SendEmailAsync("evgeni_136@abv.bg", "TopCourses", "pzpdnba094@pdfintojpg.org", course.Title, mailContent);
            this.TempData[MessageConstant.SuccessMessage] = "Email send succsessfully";
            return this.RedirectToAction("Details", "Course", new { area = "admin", id = course.Id });
        }
    }
}
