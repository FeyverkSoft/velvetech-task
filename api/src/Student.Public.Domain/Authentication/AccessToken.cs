using System;

namespace Student.Public.Domain.Authentication
{
    public sealed class AccessToken
    {
        public String Value { get; }

        public TimeSpan ExpiresIn { get; }
        
        public AccessToken(String value, TimeSpan expiresIn)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
            ExpiresIn = expiresIn;
        }

    }
}
