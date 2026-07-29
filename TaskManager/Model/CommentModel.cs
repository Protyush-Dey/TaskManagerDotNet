using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Model
{
    public class CommentModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string UserId { get; set; } = string.Empty;

        [BsonElement("TaskId")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string TaskId { get; set; } = string.Empty;

        [BsonElement("Comment")]
        [Required]
        [StringLength(500)]
        public string Comment { get; set; } = string.Empty;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}