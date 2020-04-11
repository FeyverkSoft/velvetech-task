using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Public.Domain.Authentication
{
    public sealed class UserAuthenticationService
    {
        private readonly IUserGetter _userGetter;
        private readonly IRefreshTokenStore _refreshTokenStore;
        private readonly IAccessTokenFactory _accessTokenFactory;
        private readonly IPasswordHasher _passwordHasher;

        public UserAuthenticationService(
            IUserGetter userGetter,
            IRefreshTokenStore refreshTokenStore,
            IAccessTokenFactory accessTokenFactory,
            IPasswordHasher passwordHasher)
        {
            _userGetter = userGetter;
            _refreshTokenStore = refreshTokenStore;
            _accessTokenFactory = accessTokenFactory;
            _passwordHasher = passwordHasher;
        }

        public async Task<Token> AuthenticationByPassword(String login, String password, CancellationToken cancellationToken)
        {
            var user = await _userGetter.Get(login, cancellationToken);

            if (user == null)
                throw new UnauthorizedException();

            if (!user.IsActive)
                throw new UnconfirmedException();

            if (!await _passwordHasher.VerifyHashedPassword(user.Password, password, cancellationToken))
                throw new UnauthorizedException();

            var refreshToken = await _refreshTokenStore.Add(user.Id);

            var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

            return new Token(
                accessToken: accessToken.Value,
                expiresIn: accessToken.ExpiresIn,
                refreshToken: refreshToken);
        }

        public async Task<Token> AuthenticationByRefreshToken(String refreshToken, CancellationToken cancellationToken)
        {
            var newRefreshToken = await _refreshTokenStore.Reissue(refreshToken);

            if (newRefreshToken == null)
                throw new UnauthorizedException();

            var user = await _userGetter.Get(newRefreshToken.UserId, cancellationToken);

            if (user == null)
                throw new UnauthorizedException();

            if (!user.IsActive)
                throw new UnconfirmedException();

            var accessToken = await _accessTokenFactory.Create(user, cancellationToken);

            return new Token(
                accessToken: accessToken.Value,
                expiresIn: accessToken.ExpiresIn,
                refreshToken: newRefreshToken.Value);
        }
    }
}