using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Core.Common.Auth
{
    public class AuthOptions
    {
        public const string Issuer = "http://localhost:9301/";
        public const string Audience = "https://localhost:3000/";
        const string Key = "mysupersecret_secretkey!123";
        public const int LifetimeInMinutes = 600;
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
