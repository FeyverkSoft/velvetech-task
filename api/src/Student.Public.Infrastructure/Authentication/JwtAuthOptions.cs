using System;

namespace Student.Public.Infrastructure.Authentication
{
    public sealed class JwtAuthOptions
    {
        public String Issuer { get; set; }

        public String Audience { get; set; }

        public String SecretKey { get; set; }

        public Int32 LifeTimeMinutes { get; set; }
    }
}
