namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using System.Security.Claims;
    using System.Text.Json.Nodes;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Section;
    using TopCourses.Extensions;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;

    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;
        private readonly IFileService fileService;

        public CourseController(ICourseService courseService,
                                ICategoryService categoryService,
                                ILanguageService languageService,
                                UserManager<ApplicationUser> userManager,
                                IFileService fileService,
                                ILogger<CourseController> logger)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.languageService = languageService;
            this.userManager = userManager;
            this.fileService = fileService;
            this.logger = logger;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCourses = await this.courseService.GetAll();
            return View(allCourses);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();

            var course = new AddCourseModel()
            {
                Categories = categories,
                Languages = languages,
                Section = new SectionModel()
            };

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseModel model)
        {
            var categories = await this.categoryService.GetAllMainCategories();
            if (!categories.Any(b => b.Id == model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            var languages = await this.languageService.GetAll();
            if (!languages.Any(b => b.Id == model.LanguageId))
            {
                this.ModelState.AddModelError(nameof(model.LanguageId), "Language does not exist");
            }

            if (!ModelState.IsValid)
            {
                model.Languages = languages;
                model.Categories = categories;
                return View(model);
            }

            var currentUserId = GetUserId();
            await this.courseService.CreateCourse(model, currentUserId);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var details = await this.courseService.GetCourseDetails(id);
            return View(details);
        }

        [HttpPost]
        public IActionResult CreateSection([FromBody]JsonObject jsonObj)
        {
            var model = JsonConvert.DeserializeObject<AddCourseModel>(jsonObj.ToString());
            // Check whether this request is comming with javascript, if so, we know that we are going to add contact details.
            if (Request.IsAjaxRequest())
            {
                var section = model.Section;
                //section.Title = model.Section.Title;
                //section.VideoUrl = model.Section.VideoUrl;
                //section.Description = model.Section.Description;

                if (model.Curriculum == null)
                {
                    model.Curriculum = new List<SectionModel>();
                }

                model.Curriculum.Add(section);

                return RedirectToAction("Add", "Course", model);
            }

            return BadRequest();
        }

        //todo
        public async Task<IActionResult> UploadFile(IFormFile file,  int sectionId)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await file.CopyToAsync(stream);

                        var fileToSave = new ApplicationFile()
                        {
                            FileName = file.FileName,
                            Content = stream.ToArray(),
                            ContentType = file.ContentType,
                            SourceId = sectionId
                        };
                        await fileService.SaveFile(fileToSave);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "CourseController/UploadFile");

                TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
            }

            TempData[MessageConstant.SuccessMessage] = "File uploaded successfully";
            //todo
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Video()
        {
            return View();
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
