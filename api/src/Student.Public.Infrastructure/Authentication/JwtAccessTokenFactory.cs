using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Student.Public.Domain.Authentication;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class JwtAccessTokenFactory : IAccessTokenFactory
    {
        private readonly JwtAuthOptions _authOptions;

        public JwtAccessTokenFactory(IOptions<JwtAuthOptions> options)
        {
            _authOptions = options.Value;
        }

        public async Task<AccessToken> Create(Domain.Authentication.User  user, CancellationToken cancellationToken)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_authOptions.LifeTimeMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Issuer,
                audience: _authOptions.Audience,
                notBefore: now,
                claims: claimsIdentity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey)),
                    SecurityAlgorithms.HmacSha256));

            String token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AccessToken(
                value: token,
                expiresIn: TimeSpan.FromMinutes(_authOptions.LifeTimeMinutes));
        }
    }
}
