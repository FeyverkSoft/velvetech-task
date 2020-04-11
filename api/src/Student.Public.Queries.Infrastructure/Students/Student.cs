using System;
using Student.Types;

namespace Student.Public.Queries.Infrastructure.Students
{
    internal sealed class Student
    {
        /// <summary>
        /// Идентификатор студента
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// не будем усложнять, и делать вариант что у студента может быть много менторов
        /// </summary>
        public Guid MentorId { get; }

        /// <summary>
        /// Публичный идентификатор студента
        /// </summary>
        public String PublicId { get; }

        /// <summary>
        /// Имя
        /// </summary>
        public String FirstName { get; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public String LastName { get; }

        /// <summary>
        /// Отчество
        /// </summary>
        public String SecondName { get; }

        /// <summary>
        /// Пол
        /// </summary>
        public StudentGender Gender { get; }

        /// <summary>
        /// Статус студента
        /// </summary>
        public StudentStatus Status { get; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; }
    }
}