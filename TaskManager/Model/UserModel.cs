using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TodoList.Model
{
    public enum Role
    {
        Admin,
        Manager,
        Employee
    }

    public class UserModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [BsonElement("Email")]
        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [BsonElement("PasswordHash")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&^#()_+\-=\[\]{};':"",.<>\/\\|`~])[A-Za-z\d@$!%*?&^#()_+\-=\[\]{};':"",.<>\/\\|`~]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("Role")]
        [BsonRepresentation(BsonType.String)]
        public Role UserRole { get; set; } = Role.Employee;

        [BsonElement("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("UpdatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}