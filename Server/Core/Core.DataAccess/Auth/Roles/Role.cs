using Microsoft.AspNetCore.Identity;

namespace Core.DataAccess.Auth.Roles
{
    public class Role : IdentityRole
    {
        public Role() { }
        public Role(string name) : base(name)
        {
        }
    }
}