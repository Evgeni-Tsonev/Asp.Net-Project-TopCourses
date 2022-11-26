namespace TopCourses.Controllers
{
    using System.IO;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;
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
            return this.View();
        }


        [HttpPost]
        public async Task UploadFile(IFormFile file)
        {
            var type = file.ContentType.ToString();
            var fileName = file.FileName;
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument { { "FileName", fileName }, { "Type", type } },
            };

            using var stream = await this.bucket.OpenUploadStreamAsync(fileName, options); // Open the output stream
            var id = stream.Id; // Unique Id of the file
            file.CopyTo(stream); // Copy the contents to the stream
            await stream.CloseAsync();
        }

        public async Task<IActionResult> Download(string id)
        {
            using (var client = new HttpClient())
            {
                using (var stream = this.bucket.OpenDownloadStream(new ObjectId("637ea5b20b6f886d85cbe908")))
                {
                    using (var fs = new FileStream("localfile.jpg", FileMode.OpenOrCreate))
                    {
                        //stream.BeginWrite();
                    }
                }
            }

            var filePath = @"C:\Users\Local.Admin\Downloads";
            using (var stream = this.bucket.OpenDownloadStream(new ObjectId("637ea5b20b6f886d85cbe908")))
            {
                var buffer = new byte[1024];
                var file = new StreamWriter(filePath);
                // read from stream until end of file is reached
            }

            return this.Ok();
        }
    }
}
