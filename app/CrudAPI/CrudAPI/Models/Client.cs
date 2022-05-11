using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CrudAPI.Models
{
    public class Client
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ClientId { get; set; }

        [BsonElement("First_Name")]
        public string FirstName { get; set; } = null;

        [BsonElement("Last_Name")]
        public string LastName { get; set; } = null;

        [BsonElement("Email")]
        public string Email { get; set; } = null;

        [BsonElement("Gender")]
        public string Gender { get; set; } = null;
    }
}
