using System;

namespace Student.Public.Domain.Authentication
{
    public sealed class Token
    {
        public String AccessToken { get; }

        public TimeSpan ExpiresIn { get; }

        public String RefreshToken { get; }
        
        public Token(String accessToken, TimeSpan expiresIn, String refreshToken)
        {
            AccessToken = accessToken ?? throw new ArgumentNullException(nameof(accessToken));
            ExpiresIn = expiresIn;
            RefreshToken = refreshToken;
        }
    }
}
