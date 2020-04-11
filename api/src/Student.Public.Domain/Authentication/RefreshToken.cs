using System;

namespace Student.Public.Domain.Authentication
{
    public sealed class RefreshToken
    {
        public String Value { get; }

        public Guid UserId { get; }

        public RefreshToken(String value, Guid userId)
        {
            Value = value;
            UserId = userId;
        }
    }
}