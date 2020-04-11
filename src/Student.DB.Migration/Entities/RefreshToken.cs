using System;

namespace Student.DB.Migrations.Entities
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
        public DateTime ExpireDate { get; }
    }
}