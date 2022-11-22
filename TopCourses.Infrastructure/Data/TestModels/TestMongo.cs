namespace TopCourses.Infrastructure.Data.TestModels
{
    using MongoDB.Bson.Serialization.Attributes;

    public class TestMongo
    {
        [BsonId]
        [BsonRequired]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Test { get; set; }
    }
}
