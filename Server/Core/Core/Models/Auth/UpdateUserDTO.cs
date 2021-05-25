using System.ComponentModel.DataAnnotations;

namespace Core.Models.Auth
{
    public class UpdateUserDTO
    {
        public string Id { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [Required]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Compare("Password", ErrorMessage = "Passwords are different")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = "";
    }
}
