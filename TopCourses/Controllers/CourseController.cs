namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    using System.Text.Json;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;
    using TopCourses.Core.Models.Review;
    using TopCourses.Core.Models.Topic;
    using TopCourses.Core.Models.Video;
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
            var categories = await this.categoryService.GetAllCategories();
            var languages = await this.languageService.GetAll();
            var course = new AddCourseViewModel()
            {
                Languages = languages,
                Categories = categories
            };

            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCourseViewModel model)
        {
            var categories = await this.categoryService.GetAllCategories();
            if (!categories.Any(b => b.Id == model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            var languages = await this.languageService.GetAll();
            if (!languages.Any(b => b.Id == model.LanguageId))
            {
                this.ModelState.AddModelError(nameof(model.LanguageId), "Language does not exist");
            }

            if (TempData.ContainsKey("Curriculum"))
            {
                var data = TempData["Curriculum"]?.ToString();
                var curriculum = JsonSerializer.Deserialize<ICollection<AddTopicViewModel>>(data);
                model.Curriculum = curriculum;
                TempData.Keep("Curriculum");
            }

            if (!ModelState.IsValid)
            {
                model.Languages = languages;
                model.Categories = categories;
                return View(model);
            }
            ;
            var currentUserId = GetUserId();
            await this.courseService.CreateCourse(model, currentUserId);

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var course = await this.courseService.GetCourseDetails(id);

            var url = course.Curriculum.Select(u => u.VideoUrl).FirstOrDefault();

            ViewData["Title"] = $"{course.Title}";

            ViewData["Subtitle"] = $"{course.Subtitle}";

            TempData["VideoUrl"] = url;

            return View(course);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateSection(AddCourseModel model)
        //{
        //    if (TempData.ContainsKey("Curriculum"))
        //    {
        //        var data = TempData["Curriculum"]?.ToString();
        //        var curriculum = JsonSerializer.Deserialize<ICollection<AddSectionModel>>(data);
        //        model.Curriculum = curriculum;
        //    }

        //    model.Curriculum.Add(model.Topic);
        //    model.Categories = await this.categoryService.GetAllCategories();
        //    model.Languages = await this.languageService.GetAll();

        //    return View("Add", model);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateSection(AddCourseViewModel model)
        {
            var modelData = JsonSerializer.Serialize(model);

            TempData["model"] = modelData;

            return RedirectToAction("Create", "Topic");
        }

        //todo
        public async Task<IActionResult> UploadFile(IFormFile file, int sectionId)
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

        public IActionResult Video(string videoUrl)
        {
            var video = TempData["VideoUrl"]?.ToString();
            var model = new VideoViewModel()
            {
                VideoUrl = videoUrl
            };

            return View(model);
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
