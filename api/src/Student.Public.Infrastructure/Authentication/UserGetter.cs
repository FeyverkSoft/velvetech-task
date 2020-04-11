using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Public.Domain.Authentication;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class UserGetter : IUserGetter
    {
        private readonly AuthenticationDbContext _context;

        public UserGetter(AuthenticationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(String login, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(_ => _.Login == login, cancellationToken);
        }

        public async Task<User> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }
    }
}