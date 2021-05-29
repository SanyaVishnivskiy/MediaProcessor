using System.Collections.Generic;

namespace Core.Models.Auth
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<string> Roles { get; set; }
    }
}
