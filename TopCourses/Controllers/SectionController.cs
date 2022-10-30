namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;


    public class SectionController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;

        public SectionController(ICategoryService categoryService, ILanguageService languageService)
        {
            this.categoryService = categoryService;
            this.languageService = languageService;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create(AddCourseModel model)
        //{
        //    model.Curriculum.Add(model.Section);
        //    model.Categories = await this.categoryService.GetAllMainCategories();
        //    model.Languages = await this.languageService.GetAll();

        //    return View("Add", model);
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateSection(AddCourseModel model)
        //{
        //    model.Curriculum.Add(model.Section);
        //    model.Categories = await this.categoryService.GetAllMainCategories();
        //    model.Languages = await this.languageService.GetAll();

        //    return View("Add", model);
        //}
    }
}
