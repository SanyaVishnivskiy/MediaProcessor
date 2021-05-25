using AutoMapper;
using Core.Business.Auth.Models;
using Core.Business.Models;
using Core.Common.Auth;
using Core.DataAccess.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public class AuthComponent : IAuthComponent
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthComponent(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        public async Task<LoginResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.EmployeeId);
            if (user is null)
            {
                return LoginResult.CreateFailed();
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!isPasswordCorrect)
            {
                return LoginResult.CreateFailed();
            }

            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            var token = CreateToken(principal);
            return LoginResult.CreateSucceeded(token);
        }

        private string CreateToken(ClaimsPrincipal principal)
        {
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                notBefore: now,
                claims: principal.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LifetimeInMinutes)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
