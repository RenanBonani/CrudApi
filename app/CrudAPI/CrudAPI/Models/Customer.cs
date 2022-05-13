using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace CrudAPI.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MinLength(3,ErrorMessage = "Required Name")]
        [BsonElement("first_name")]
        public string FirstName { get; set; } = null;

        [Required]
        [BsonElement("last_name")]
        public string LastName { get; set; } = null;

        [Required]
        [BsonElement("email")]
        public string Email { get; set; } = null;

        [Required]
        [BsonElement("gender")]
        public string Gender { get; set; } = null;
    }
}
