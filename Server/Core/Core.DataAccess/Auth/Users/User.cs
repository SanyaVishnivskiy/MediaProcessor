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
    }
}
