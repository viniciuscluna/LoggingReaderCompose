using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ViniciusTestLog.API.Domain
{
    public class LogData
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public DateTime Date { get; set; }
        public string? Ip { get; set; }
        public string? Category { get; set; }
        public string? Message { get; set; }
    }
}
