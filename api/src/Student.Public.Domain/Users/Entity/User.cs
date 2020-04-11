using System;
using Student.Types;

namespace Student.Public.Domain.Users.Entity
{
    public sealed class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; private set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата когда пользователь был удалён
        /// </summary>
        public DateTime? DeletedDate { get; private set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public String Name { get; private set; }

        /// <summary>
        /// Статус пользователя
        /// </summary>
        public UserStatus Status { get; private set; } = UserStatus.Active;

        /// <summary>
        /// Логин для авторизации
        /// </summary>
        public String Login { get; private set; }

        /// <summary>
        /// Пароль для авторизации
        /// </summary>
        public String Password { get; private set; }

        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();

        protected User() { }

        internal User(String login, String name, String password)
            => (Login, Name, Password)
                = (login, name, password); // в идеальном мире тут бы отправить комануду в шину типа UserCreated

        /*
        /// <summary>
        /// Установка пользователю нового пароля
        /// </summary>
        /// <param name="newPassword"></param>
        internal void ChangePassword(String newPassword)
        {
            if (String.IsNullOrEmpty(newPassword) || newPassword == Password)
                throw new WrongPasswordException("Incorrect password");

            Password = newPassword;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }    
        */
    }
}