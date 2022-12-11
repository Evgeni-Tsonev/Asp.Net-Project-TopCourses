namespace TopCourses.Areas.Admin.Controllers
{
    using Ganss.Xss;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Language;

    public class LanguageController : BaseController
    {
        private readonly ILanguageService languageService;
        private readonly ILogger logger;

        public LanguageController(
            ILanguageService languageService,
            ILogger<LanguageController> logger)
        {
            this.languageService = languageService;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var languages = await this.languageService.GetAll();

            return this.View(languages);
        }

        public IActionResult Add()
        {
            var model = new LanguageViewModel();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LanguageViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
            model.Title = sanitizer.Sanitize(model.Title);
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                await this.languageService.Add(model);
                this.TempData[MessageConstant.SuccessMessage] = "Syccsessfully added language";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "LanguageController/Add");
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.languageService.GetLanguageForEdit(id);

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LanguageViewModel model)
        {
            var sanitizer = new HtmlSanitizer();
            model.Title = sanitizer.Sanitize(model.Title);
            if (!this.ModelState.IsValid)
            {
                return this.View("Edit", model);
            }

            try
            {
                await this.languageService.Update(model);
                this.TempData[MessageConstant.SuccessMessage] = "Syccsessfully updated language";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "LanguageController/Update");
            }

            return this.RedirectToAction("Index", "Language");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int id)
        {
            try
            {
                await this.languageService.Delete(id);
                this.TempData[MessageConstant.SuccessMessage] = "Syccsessfully deleted language";
            }
            catch (Exception ex)
            {
                this.TempData[MessageConstant.ErrorMessage] = ex.Message;
                this.logger.LogError(ex, "LanguageController/Delete");
            }

            return this.RedirectToAction("Index", "Language");
        }
    }
}
