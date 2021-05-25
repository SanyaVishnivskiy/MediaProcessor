using Core.DataAccess.Auth;
using Core.DataAccess.Auth.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Core.Business.Auth.Initializer
{
    public class AuthInitializer
    {
        private const string AdminEmployeeId = "admin";
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "pass123";// TODO move to env variables

        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthInitializer(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task InitializeAsync()
        {
            if (await _roleManager.FindByNameAsync("admin") == null)
            {
                await _roleManager.CreateAsync(new Role("admin"));
            }
            if (await _roleManager.FindByNameAsync("employee") == null)
            {
                await _roleManager.CreateAsync(new Role("employee"));
            }

            if (await _userManager.FindByNameAsync(AdminEmployeeId) == null)
            {
                var admin = new User { Email = AdminEmail, UserName = AdminEmployeeId };
                var result = await _userManager.CreateAsync(admin, AdminPassword);
                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(admin, new[] { "admin", "employee" });
                }
            }
        }
    }
}
