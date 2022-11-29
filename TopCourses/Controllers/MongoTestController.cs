namespace TopCourses.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;
    using SharpCompress.Compressors.Xz;
    using System.IO;
    using TopCourses.Core.Constants;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;

    public class MongoTestController : Controller
    {
        private readonly GridFSBucket bucket;
        private readonly ILogger logger;

        public MongoTestController(IBucket bucketContex, ILogger<MongoTestController> logger)
        {
            this.logger = logger;
            this.bucket = bucketContex.Create();
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task UploadFile(IFormFileCollection files)
        {
            var filesIds = new List<string>();
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

                        //using (var str = new MemoryStream())
                        //{
                        //    var test = file.CopyToAsync(str);
                        //    byte[] data = str.ToArray();
                        //    await this.bucket.UploadFromBytesAsync(fileName, data, options);
                        //}

                        using (var stream = await this.bucket.OpenUploadStreamAsync(fileName, options))
                        {
                            await file.CopyToAsync(stream);
                            filesIds.Add(stream.Id.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.logger.LogError(ex, "CourseController/UploadFile");
                    this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
                }
            }
        }
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    try
        //    {
        //        if (file != null && file.Length > 0)
        //        {
        //            using (var stream = new MemoryStream())
        //            {
        //                await file.CopyToAsync(stream);
        //                var fileToSave = new ApplicationFile()
        //                {
        //                    FileName = file.FileName,
        //                    Content = stream.ToArray(),
        //                    ContentType = file.ContentType,
        //                };
        //                await this.fileService.SaveFile(fileToSave);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.logger.LogError(ex, "CourseController/UploadFile");

        //        this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
        //    }

        //    this.TempData[MessageConstant.SuccessMessage] = "File uploaded successfully";
        //    //todo
        //    return this.RedirectToAction(nameof(this.Index));
        //}

        public async Task<IActionResult> Download(string id)
        {
            var stream = await this.bucket.OpenDownloadStreamAsync(new ObjectId("6383ad2f3d5004fba5fa70ba"));
            var fileName = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "FileName");
            var fileType = stream.FileInfo.Metadata.FirstOrDefault(x => x.Name == "Type");
            return this.File(stream, fileType.Value.ToString(), fileName.Value.ToString());
        }
    }
}