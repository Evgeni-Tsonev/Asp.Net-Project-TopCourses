namespace TopCourses.Infrastructure.Data.MongoInterfaceses
{
    using MongoDB.Driver.GridFS;

    public interface IBucket
    {
        GridFSBucket Create();
    }
}
