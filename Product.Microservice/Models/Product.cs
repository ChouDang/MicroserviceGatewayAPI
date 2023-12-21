using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Product.Microservice.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } = 0;
    }
}
