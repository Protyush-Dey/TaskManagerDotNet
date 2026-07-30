using System.ComponentModel.DataAnnotations;
using TodoList.Model;

namespace TaskManager.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&^#()_+\-=\[\]{};':"",.<>\/\\|`~])[A-Za-z\d@$!%*?&^#()_+\-=\[\]{};':"",.<>\/\\|`~]{8,}$", ErrorMessage = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; } = string.Empty;

        [Required]
        public Role UserRole { get; set; } = Role.Employee;
    }
}
