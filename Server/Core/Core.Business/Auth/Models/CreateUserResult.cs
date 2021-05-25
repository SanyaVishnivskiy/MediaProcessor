using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace Core.Business.Auth.Models
{
    public record CreateUserResult
    {
        public bool Succeeded { get; init; }
        public List<string> Errors { get; init; }

        public CreateUserResult() { }

        public CreateUserResult(IdentityResult result)
        {
            Succeeded = result.Succeeded;
            Errors = result.Errors.Select(x => x.Description).ToList();
        }
    }

    public record UpdateUserResult : CreateUserResult
    {
        public UpdateUserResult() { }

        public UpdateUserResult(IdentityResult result)
        {
            Succeeded = result.Succeeded;
            Errors = result.Errors.Select(x => x.Description).ToList();
        }
    }
}
