using System;
using Student.Types;

namespace Student.Public.Domain.Authentication
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public UserStatus Status { get; } 

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get;  }

        public Boolean IsActive => Status == UserStatus.Active;

        protected User() { }

    }
}