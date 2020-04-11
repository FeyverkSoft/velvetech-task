using System;

namespace Student.Public.Infrastructure.Authentication
{
    internal sealed class RefreshToken
    {
        public String Id { get; private set; }

        /// <summary>
        /// Идентификатор пользователя которому пренадлежит RefreshToken
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Дата стухания сессии
        /// </summary>
        public DateTime ExpireDate { get; private set; }
        
        private RefreshToken() { }
        public RefreshToken(String id, Guid userId, TimeSpan expiresIn)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            UserId = userId;
            ExpireDate = DateTime.UtcNow.Add(expiresIn);
        }

        public void Terminate()
        {
            ExpireDate = DateTime.UtcNow;
        }
    }
}