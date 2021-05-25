using Core.DataAccess.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Business.Auth.Component
{
    public class UserManager : UserManager<User>
    {
        public UserManager(
            IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }

        public async Task<IdentityResult> ChangePasswordAsync(User user, string newPassword, CancellationToken token = default)
        {

            var store = this.Store as IUserPasswordStore<User>;
            if (store == null)
            {
                var errors = new IdentityError[]
                {
                    new IdentityError { Description = "Current UserStore doesn't implement IUserPasswordStore" }
                };

                return IdentityResult.Failed(errors);
            }

            if (PasswordValidators.Any())
            {
                var tasks = PasswordValidators.Select(x => x.ValidateAsync(this, user, newPassword));
                var results = await Task.WhenAll(tasks);
                if (results.Any(x => !x.Succeeded))
                    return results.First(x => !x.Succeeded);
            }

            var newPasswordHash = PasswordHasher.HashPassword(user, newPassword);

            await store.SetPasswordHashAsync(user, newPasswordHash, token);
            return IdentityResult.Success;
        }
    }
}
