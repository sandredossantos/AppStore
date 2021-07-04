using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AppStore.Domain.Entities
{
    public class Purchase
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TaxNumber { get; set; }
        public string Code { get; set; }
        public string CardNumber { get; set; }
        public string ValidThru { get; set; }
        public long SecurityCode { get; set; }        
        public string Status { get; set; }
    }
}