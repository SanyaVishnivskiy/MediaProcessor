using System.Collections.Generic;

namespace Core.Business.Auth.Models
{
    public class CreateUserModel
    {
        public string EmployeeId { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<string> Roles { get; set; }
    }
}
