using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Public.Domain.Authentication
{
    public interface IUserGetter
    {
        Task<User> Get(String login, CancellationToken cancellationToken);
        Task<User> Get(Guid id, CancellationToken cancellationToken);
    }
}
