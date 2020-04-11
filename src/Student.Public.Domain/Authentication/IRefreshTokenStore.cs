using System;
using System.Threading.Tasks;

namespace Student.Public.Domain.Authentication
{
    public interface IRefreshTokenStore
    {
        Task<String> Add(Guid userId);

        Task<RefreshToken> Reissue(String refreshToken);
    }
}
