namespace TopCourses.Controllers
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.Models;
    using TopCourses.Models;

    public class CourseController : BaseController
    {
        private readonly ILogger<CourseController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;
        private readonly IFileService fileService;

        public CourseController(
                                ICourseService courseService,
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
        public async Task<IActionResult> Index([FromQuery] AllCoursesQueryModel query)
        {
            var result = await this.courseService.GetAll(
                query.Category,
                query.SubCategory,
                query.SearchTerm,
                query.Language,
                query.MinPrice,
                query.MaxPrice,
                query.CurrentPage,
                AllCoursesQueryModel.CoursesPerPage,
                query.Sorting);

            var allCategories = await this.categoryService.GetAllCategories();

            query.Categories = allCategories.Where(c => c.ParentId == null);
            query.Languages = await this.languageService.GetAll();
            query.Courses = result;

            return this.View(query);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.categoryService.GetAllCategories();
            var languages = await this.languageService.GetAll();
            var course = new AddCourseViewModel()
            {
                Languages = languages,
                Categories = categories,
            };

            return this.View(course);
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

            if (!this.ModelState.IsValid)
            {
                model.Languages = languages;
                model.Categories = categories;
                return this.View(model);
            }

            var currentUserId = this.GetUserId();
            await this.courseService.CreateCourse(model, currentUserId);
            return this.RedirectToAction(nameof(this.Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            var course = await this.courseService.GetCourseDetails(id);

            this.ViewData["Title"] = $"{course.Title}";
            this.ViewData["Subtitle"] = $"{course.Subtitle}";

            return this.View(course);
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
                            SourceId = sectionId,
                        };
                        await this.fileService.SaveFile(fileToSave);
                    }
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "CourseController/UploadFile");

                this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
            }

            this.TempData[MessageConstant.SuccessMessage] = "File uploaded successfully";
            //todo
            return this.RedirectToAction(nameof(this.Index));
        }

        private string GetUserId()
            => this.User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
