using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Public.Domain.Users;
using Student.Public.Domain.Users.Entity;

namespace Student.Public.Infrastructure.Users
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<User> Get(String login, CancellationToken cancellationToken)
        {
            return await _context.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(_ => _.Login == login, cancellationToken);
        }

        public async Task Save(User user, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                _context.Users.Add(user);

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}