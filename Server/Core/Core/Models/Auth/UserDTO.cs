using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Models.Auth
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string EmployeeId { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
