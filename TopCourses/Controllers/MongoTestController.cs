namespace TopCourses.Controllers
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;

    public class MongoTestController : Controller
    {
        private readonly GridFSBucket bucket;

        public MongoTestController(IBucket bucketContex)
        {
            this.bucket = bucketContex.Create();
        }

        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> UploadFile(IFormFile file)
        //{
        //    try
        //    {
        //        if (file != null && file.Length > 0)
        //        {
        //            using (var stream = new MemoryStream())
        //            {
        //                await file.CopyToAsync(stream);

        //                var fileToSave = new Infrastructure.Data.Models.ApplicationFile()
        //                {
        //                    FileName = file.FileName,
        //                    Content = stream.ToArray(),
        //                    ContentType = file.ContentType,
        //                };
        //                //await this.fileService.SaveFile(fileToSave);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //this.logger.LogError(ex, "CourseController/UploadFile");

        //        this.TempData[MessageConstant.ErrorMessage] = "A problem occurred while recording";
        //    }

        //    this.TempData[MessageConstant.SuccessMessage] = "File uploaded successfully";
        //    //todo
        //    return this.Ok(nameof(this.Index));
        //}


        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            var type = file.ContentType.ToString();
            var fileName = file.FileName;

            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument { { "FileName", fileName }, { "Type", type } }
            };

            using var stream = await this.bucket.OpenUploadStreamAsync(fileName, options); // Open the output stream
            var id = stream.Id; // Unique Id of the file
            file.CopyTo(stream); // Copy the contents to the stream
            await stream.CloseAsync();
        }

//        public async Task<IActionResult> Download(string id)
//        {
//            var filePath = @"C:\Users\Local.Admin\Downloads";
//            using (var stream = await bucket.OpenDownloadStreamAsync(id))
//            {

//                IFileProvider provider = new PhysicalFileProvider(filePath); // Initialize the Provider

//                var readStream = stream.Read(); // Extact the Stream
//                var mimeType = "application/octet-stream"; // Set a mimeType
//            }

//            var destination = new MemoryStream();


//            await bucket.DownloadToStreamAsync(id, destination);
//            //var file = GetFileByIdAsync(fileName);
//            return Ok()
//;
//        }

        //public void CopyStream(Stream stream, string destPath)
        //{
        //    using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
        //    {
        //        stream.CopyTo(fileStream);
        //    }
        //}

        //public async Task<byte[]> GetFileByIdAsync(string fileName)
        //{
        //    var fileInfo = await FindFile(fileName);
        //    return await this.bucket.DownloadAsBytesAsync(fileInfo.Id);

        //}

        //private async Task<GridFSFileInfo> FindFile(string fileName)
        //{
        //    var options = new GridFSFindOptions
        //    {
        //        Limit = 1
        //    };
        //    var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, fileName);
        //    using var cursor = await bucket.FindAsync(filter, options);
        //    return (await cursor.ToListAsync()).FirstOrDefault();
        //}

        //public GridFSBucket CreateBucket()
        //{
        //    var options = new GridFSBucketOptions
        //    {
        //        BucketName = "filesBucket",
        //        ChunkSizeBytes = 255 * 1024, //255 MB is the default value
        //    };

        //    return new GridFSBucket(this.db, options);
        //}
    }
}
