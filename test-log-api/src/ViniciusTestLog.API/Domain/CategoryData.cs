using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ViniciusTestLog.API.Domain
{
    public class CategoryData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
