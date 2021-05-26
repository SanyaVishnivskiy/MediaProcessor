using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Core.DataAccess.Auth
{
    public class User : IdentityUser
    {
        [MaxLength(50)]
        [Required]
        public string EmployeeId
        {
            get => UserName;
            set => UserName = value;
        }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }
    }
}
