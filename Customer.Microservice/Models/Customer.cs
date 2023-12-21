using Customer.Microservice.Enum;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Customer.Microservice.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public bool  IsVIP { get; set; }
        public ETypeVIP TypeVIP { get; set; } = ETypeVIP.None;
    }
}
