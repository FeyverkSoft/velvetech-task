using System;
using Student.Types;

namespace Student.Public.Domain.Students.Entity
{
    public sealed class Student
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
        /// Дата последнего изменения записи
        /// </summary>
        public DateTime UpdateDate { get; private set; }

        /// <summary>
        /// Дата создания записи
        /// </summary>
        public DateTime CreateDate { get; } = DateTime.UtcNow;

        /// <summary>
        /// Статус студента
        /// </summary>
        public StudentStatus Status { get; private set; }

        public Guid ConcurrencyTokens { get; private set; } = Guid.NewGuid();
        
        internal Student(Guid id, Guid mentorId, String publicId, String firstName, String lastName, String secondName, StudentGender gender)
        => (Id, MentorId, PublicId, FirstName, LastName, SecondName, Gender)
            = (id, mentorId, publicId, firstName, lastName, secondName, gender);

        public void MarkAsDeleted()
        {
            if (Status == StudentStatus.Deleted)
                return;
            Status = StudentStatus.Deleted;
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}