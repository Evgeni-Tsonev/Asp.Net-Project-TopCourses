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
        private readonly IViewRenderService renderer;

        public EmailController(ICourseService courseService, IEmailSender emailService, IViewRenderService renderer)
        {
            this.courseService = courseService;
            this.emailService = emailService;
            this.renderer = renderer;
        }

        [HttpPost]
        public async Task<IActionResult> SendToEmail(int id)
        {
            var course = await this.courseService.GetCourseDetails(id);
            var view = await this.renderer.RenderToString("~/Views/Course/Details.cshtml", course);

            await this.emailService.SendEmailAsync("evgeni_136@abv.bg", "TopCourses", "knghtdark0@gmail.com", course.Title, view);

            this.TempData[MessageConstant.SuccessMessage] = "Email send succsessfully";
            return this.RedirectToAction("Details", "Course", new { area = "admin", id = course.Id });
        }

        //    public MyController(IViewRendere renderer) => this.renderer = renderer;
        //    public async Task<IActionResult> MyViewTest
        //    {
        //        var view = await this.renderer.RenderAsync(this, "MyView", model);
        //return new OkObjectResult(view);

    }
}
