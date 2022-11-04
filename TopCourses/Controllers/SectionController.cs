namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Text.Json;
    using System.Text.Json.Nodes;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Section;

    public class SectionController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;

        public SectionController(ICategoryService categoryService, ILanguageService languageService)
        {
            this.categoryService = categoryService;
            this.languageService = languageService;
        }

        public IActionResult Create()
        {
            var model = new AddSectionModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddSection(AddSectionModel model)
        {
            //model.Curriculum.Add(model.Section);
            //model.Categories = await this.categoryService.GetAllMainCategories();
            //model.Languages = await this.languageService.GetAll();

            return View("Add", model);
        }

        public IActionResult Test()
        {
            var model = new AddSectionModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult SectionTest([FromBody] JsonObject jsonObj)
        {
            var model = JsonSerializer.Deserialize<AddSectionModel>(jsonObj, 
                                                                    new JsonSerializerOptions
                                                                    {
                                                                        PropertyNameCaseInsensitive = true
                                                                    });

            var list = new List<AddSectionModel>();
            list.Add(model);

            TempData["Curiculum"] = JsonSerializer.Serialize(list);

            return Ok();
        }
    }
}
