using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class RefreshTokenStore : Domain.Authentication.IRefreshTokenStore
    {
        private readonly TimeSpan _lifeTime = TimeSpan.FromDays(1);
        private readonly RefreshTokenStoreDbContext _context;

        public RefreshTokenStore(RefreshTokenStoreDbContext context)
        {
            _context = context;
        }

        public async Task<String> Add(Guid userId)
        {
            var refreshToken = new RefreshToken(Guid.NewGuid().ToString(), userId, _lifeTime);
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();
            return refreshToken.Id;
        }

        public async Task<Domain.Authentication.RefreshToken> Reissue(string refreshToken)
        {
            var oldRefreshToken = await _context.RefreshTokens
                .SingleOrDefaultAsync(token => token.Id == refreshToken &&
                                               token.ExpireDate > DateTime.UtcNow);

            if (oldRefreshToken == null)
                return null;

            oldRefreshToken.Terminate();

            var newRefreshToken = new RefreshToken(Guid.NewGuid().ToString(), oldRefreshToken.UserId, _lifeTime);

            _context.RefreshTokens.Add(newRefreshToken);

            await _context.SaveChangesAsync();

            return new Domain.Authentication.RefreshToken(newRefreshToken.Id, newRefreshToken.UserId);
        }
    }
}