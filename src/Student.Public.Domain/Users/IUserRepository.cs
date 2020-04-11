using System;
using System.Threading;
using System.Threading.Tasks;
using Student.Public.Domain.Users.Entity;

namespace Student.Public.Domain.Users
{
    public interface IUserRepository
    {
        Task<User> Get(String login, CancellationToken cancellationToken);

        Task Save(User user, CancellationToken cancellationToken);
    }
}
