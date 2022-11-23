namespace TopCourses.Infrastructure.Data
{
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using MongoDB.Driver.GridFS;
    using TopCourses.Infrastructure.Data.MongoInterfaceses;

    public class BucketContex : IBucket
    {
        public readonly IMongoDatabase db;

        public BucketContex(IOptions<MongoDbSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            this.db = client.GetDatabase(options.Value.Database);
        }

        public GridFSBucket Create()
        {
            var options = new GridFSBucketOptions
            {
                BucketName = "files",
                ChunkSizeBytes = 255 * 1024, //255 MB is the default value
            };
            return new GridFSBucket(this.db, options);
        }
    }
}
