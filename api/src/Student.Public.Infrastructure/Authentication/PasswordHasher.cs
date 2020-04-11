using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class PasswordHasher :
        Domain.Authentication.IPasswordHasher,
        Domain.Users.IPasswordHasher
    {
        private sealed class User { }

        private readonly IPasswordHasher<User> _hasher = new PasswordHasher<User>();

        public async Task<String> HashPassword(String password)
        {
            return _hasher.HashPassword(new User(), password);
        }

        public async Task<Boolean> VerifyHashedPassword(String hashedPassword, String providedPassword, CancellationToken cancellationToken)
        {
            var result = _hasher.VerifyHashedPassword(new User(), hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}