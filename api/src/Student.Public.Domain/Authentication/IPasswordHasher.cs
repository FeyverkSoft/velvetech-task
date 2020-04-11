using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Public.Domain.Authentication
{
    public interface IPasswordHasher
    {
        Task<Boolean> VerifyHashedPassword(String hashedPassword, String providedPassword, CancellationToken cancellationToken);
    }
}
