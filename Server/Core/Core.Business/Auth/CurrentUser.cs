using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Core.Business.Auth
{
    public interface ICurrentUser
    {
        string EmployeeId { get; }
    }

    public interface IMutableCurrentUser : ICurrentUser
    {
        void SetClaims(List<Claim> claims);
    }

    public class CurrentUser : IMutableCurrentUser
    {
        private readonly IHttpContextAccessor _accessor;
        private List<Claim> _claims;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public void SetClaims(List<Claim> claims)
        {
            _claims = claims ?? new List<Claim>();
        }

        public string EmployeeId
        {
            get
            {
                return FindClaim(ClaimTypes.Name) ?? "";
            }
        }

        private string FindClaim(string claimName)
        {
            if (_claims?.Any() == true)
            {
                return _claims.FirstOrDefault(x => x.Type == claimName)?.Value;
            }

            var claims = _accessor.HttpContext?.User?.Claims;
            if (claims is null)
                return null;

            return claims.FirstOrDefault(x => x.Type == claimName)?.Value;
        }
    }
}
