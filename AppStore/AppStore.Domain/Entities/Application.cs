using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AppStore.Domain.Entities
{
    public class Application
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public string Code { get; set; }
    }
}