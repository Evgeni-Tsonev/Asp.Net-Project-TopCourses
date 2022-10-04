namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models;

    public class LanguageController : Controller
    {
        private readonly ILanguageService languageService;

        public LanguageController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        public async Task<IActionResult> Index()
        {
            var languages = await languageService.GetAll();

            return View(languages);
        }


        public IActionResult Add()
        {
            var model = new LanguageModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(LanguageModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await languageService.Add(model);
            return RedirectToAction(nameof(Index));
        }
    }
}
