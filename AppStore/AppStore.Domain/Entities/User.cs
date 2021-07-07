using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace AppStore.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Sex { get; set; }
        public DateTime BirthDate { get; set; }
        public Address Address { get; set; }
        public DateTime CreationDate { get; set; }
        public User()
        {
            Address = new Address();
        }
    }
}