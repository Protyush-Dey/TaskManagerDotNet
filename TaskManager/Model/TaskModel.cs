using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Model
{
    public enum TaskStatus
    {
        Todo,
        InProgress,
        InReview,
        Completed,
        Cancelled
    }

    public class TaskModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Task")]
        [Required]
        [StringLength(200)]
        public string Task { get; set; } = string.Empty;

        [BsonElement("Desc")]
        [Required]
        public string Desc { get; set; } = string.Empty;

        [BsonElement("Status")]
        [BsonRepresentation(BsonType.String)]
        public TaskStatus Status { get; set; } = TaskStatus.Todo;

        [BsonElement("AssignedTo")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public List<string> AssignedTo { get; set; } = new();

        [BsonElement("AssignedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public string AssignedBy { get; set; } = string.Empty;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("DueDate")]
        public DateTime? DueDate { get; set; }
    }
}