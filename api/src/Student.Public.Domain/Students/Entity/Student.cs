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
        public String PublicId { get; private set; }

        /// <summary>
        /// Имя
        /// </summary>
        public String FirstName { get; private set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public String LastName { get; private set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public String SecondName { get; private set; }

        /// <summary>
        /// Пол
        /// </summary>
        public StudentGender Gender { get; private set; }

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

        public void Update(String firstName, String lastName, String secondName, StudentGender gender, String publicId)
        {
            if (FirstName == firstName &&
                LastName == lastName &&
                SecondName == secondName &&
                Gender == gender &&
                PublicId == publicId)
                return;

            FirstName = firstName;
            LastName = lastName;
            SecondName = secondName;
            Gender = gender;
            PublicId = publicId;
            
            UpdateDate = DateTime.UtcNow;
            ConcurrencyTokens = Guid.NewGuid();
        }
    }
}