namespace TopCourses.Controllers
{
    using System.Drawing;
    using System.IO;
    using Ganss.Xss;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;
    using MongoDB.Driver.GridFS;
    using TopCourses.Core.Constants;
    using TopCourses.Core.Contracts;
    using TopCourses.Core.Models.ApplicationFile;
    using TopCourses.Core.Models.Course;
    using TopCourses.Infrastructure.Data.Identity;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;
    using TopCourses.Models;

    public class CourseController : BaseController
    {
        private readonly ILogger logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICourseService courseService;
        private readonly ICategoryService categoryService;
        private readonly ILanguageService languageService;
        private readonly IFileService fileService;
        private readonly GridFSBucket bucket;

        public CourseController(
                                ICourseService courseService,
                                ICategoryService categoryService,
                                ILanguageService languageService,
                                UserManager<ApplicationUser> userManager,
                                IFileService fileService,
                                ILogger<CourseController> logger,
                                IBucket bucketContex)
        {
            this.courseService = courseService;
            this.categoryService = categoryService;
            this.languageService = languageService;
            this.userManager = userManager;
            this.fileService = fileService;
            this.logger = logger;
            this.bucket = bucketContex.Create();
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

            var allCategories = await this.categoryService.GetAllMainCategories();

            query.Categories = allCategories.Where(c => c.ParentId == null);
            query.Languages = await this.languageService.GetAll();
            query.Courses = result;
            query.TotalCoursesCount = result.FirstOrDefault() != null ? result.FirstOrDefault().TotalCoursesCount : 0;

            return this.View(query);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();
            var course = new AddCourseViewModel()
            {
                Languages = languages,
                Categories = categories,
            };

            return this.View(course);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Add(
            AddCourseViewModel model,
            [FromForm] IFormFile image)
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();

            var senitizer = new HtmlSanitizer();
            model.Title = senitizer.Sanitize(model.Title);
            model.Subtitle = senitizer.Sanitize(model.Subtitle);
            model.Description = senitizer.Sanitize(model.Description);
            model.Goals = senitizer.Sanitize(model.Goals);
            model.Requirements = senitizer.Sanitize(model.Requirements);

            foreach (var topic in model.Curriculum)
            {
                topic.Title = senitizer.Sanitize(topic.Title);
                topic.Description = senitizer.Sanitize(topic.Description);
                foreach (var video in topic.Videos)
                {
                    video.Title = senitizer.Sanitize(video.Title);
                    video.VideoUrl = senitizer.Sanitize(video.VideoUrl);
                }
            }

            if (image != null)
            {
                if (image.Length > 2097152)
                {
                    model.Languages = languages;
                    model.Categories = categories;
                    this.TempData["Error"] = "The file is too large.";
                    return this.View(model);
                }

                string[] acceptedExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".tif" };
                if (!acceptedExtensions.Contains(Path.GetExtension(image.FileName)))
                {
                    model.Languages = languages;
                    model.Categories = categories;
                    this.TempData["Error"] = "Error: Unsupported file!";
                    return this.View(model);
                }

                try
                {
                    if (image != null && image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await image.CopyToAsync(ms);
                            model.Image = ms.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "CourseController/UploadFile");
                    this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
                }
            }
            else
            {
                model.Languages = languages;
                model.Categories = categories;
                this.TempData["Error"] = "Field Image is required";
                return this.View(model);
            }

            if (!categories.Any(b => b.Id == model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!languages.Any(b => b.Id == model.LanguageId))
            {
                this.ModelState.AddModelError(nameof(model.LanguageId), "Language does not exist");
            }

            foreach (var topic in model.Curriculum)
            {
                var files = await this.UploadFile(topic.Files);
                topic.FilesInfo = files;
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

        public async Task<IActionResult> MyLearning()
        {
            var model = new MyLearningViewModel();
            var userId = this.GetUserId();
            model.CoursesEnrolled = await this.courseService.GetAllEnroledCourses(userId);
            model.CoursesCreated = await this.courseService.GetAllCreatedCourses(userId);
            model.ArchivedCourses = await this.courseService.GetAllArchivedCourses(userId);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromForm] int courseId)
        {
            var userId = this.GetUserId();
            await this.courseService.Delete(courseId, userId);
            return this.RedirectToAction("MyLearning");
        }

        public async Task<IActionResult> Edit(int courseId)
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();
            var model = await this.courseService.GetCourseToEdit(courseId);
            model.Languages = languages;
            model.Categories = categories;

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditCourseViewModel model, IFormFile image)
        {
            var categories = await this.categoryService.GetAllMainCategories();
            var languages = await this.languageService.GetAll();

            if (image != null)
            {
                if (image.Length > 2097152)
                {
                    model.Languages = languages;
                    model.Categories = categories;
                    this.TempData["Error"] = "The file is too large.";
                    return this.View("Edit", model);
                }

                string[] acceptedExtensions = { ".png", ".jpg", ".jpeg", ".gif", ".tif" };
                if (!acceptedExtensions.Contains(Path.GetExtension(image.FileName)))
                {
                    model.Languages = languages;
                    model.Categories = categories;
                    this.TempData["Error"] = "Error: Unsupported file!";
                    return this.View("Edit", model);
                }

                try
                {
                    if (image != null && image.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            await image.CopyToAsync(ms);
                            model.Image = ms.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "CourseController/UploadFile");
                    this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
                }
            }

            if (!categories.Any(b => b.Id == model.CategoryId))
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist");
            }

            if (!languages.Any(b => b.Id == model.LanguageId))
            {
                this.ModelState.AddModelError(nameof(model.LanguageId), "Language does not exist");
            }

            var senitizer = new HtmlSanitizer();
            model.Title = senitizer.Sanitize(model.Title);
            model.Subtitle = senitizer.Sanitize(model.Subtitle);
            model.Description = senitizer.Sanitize(model.Description);
            model.Goals = senitizer.Sanitize(model.Goals);
            model.Requirements = senitizer.Sanitize(model.Requirements);

            this.ModelState.Remove("Image");
            if (!this.ModelState.IsValid)
            {
                model.Languages = languages;
                model.Categories = categories;
                return this.View("Edit", model);
            }

            var currentUserId = this.GetUserId();
            await this.courseService.Update(model, currentUserId);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Download(string id, int courseId)
        {
            var userId = this.GetUserId();
            var doUserHavePermission = await this.courseService.DoUserHavePermission(userId, courseId);
            if (!doUserHavePermission)
            {
                this.TempData[MessageConstant.WarningMessage] = "You do not have access to this resource.";
                return this.RedirectToAction("Details", "Course", new { Id = courseId });
            }

            var stream = await this.bucket.OpenDownloadStreamAsync(new ObjectId(id));
            var fileName = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "FileName");
            var fileType = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "Type");
            return this.File(stream, fileType.Value.ToString(), fileName.Value.ToString());
        }

        private async Task<ICollection<FileViewModel>> UploadFile(ICollection<IFormFile> files)
        {
            var filesToReturn = new List<FileViewModel>();
            foreach (var file in files)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var type = file.ContentType.ToString();
                        var fileName = file.FileName;
                        var options = new GridFSUploadOptions
                        {
                            Metadata = new BsonDocument { { "FileName", fileName }, { "Type", type } },
                        };

                        using (var stream = await this.bucket.OpenUploadStreamAsync(fileName, options))
                        {
                            await file.CopyToAsync(stream);

                            var fileToReturn = new FileViewModel()
                            {
                                FileName = file.FileName,
                                SourceId = stream.Id.ToString(),
                                ContentType = file.ContentType,
                                FileLength = stream.Length,
                            };

                            filesToReturn.Add(fileToReturn);
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "CourseController/UploadFile");
                    this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
                }
            }

            return filesToReturn;
        }
    }
}
