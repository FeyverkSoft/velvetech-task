using System;
using System.Collections.Generic;
using Student.Types;

namespace Student.DB.Migrations.Entities
{
    internal sealed class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; }
        
        /// <summary>
        /// Список студентов пользователя
        /// </summary>
        public List<Student> Students { get; }
        
        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }

        /// <summary>
        /// Дата обновления записи
        /// </summary>
        public DateTime UpdateDate { get; }

        /// <summary>
        /// Дата когда пользователь был удалён
        /// </summary>
        public DateTime? DeletedDate { get; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public String Name { get; }
        
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
        public String Password { get; }
        public Guid ConcurrencyTokens { get; }

        protected User() { }
        public User(Guid id, string login, string name, UserStatus status, DateTime createDate, DateTime updateDate)
            => (Id, Login, Name, Status, CreateDate, UpdateDate)
            = (id, login, name, status, createDate, updateDate);
    }
}
