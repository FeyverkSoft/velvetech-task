using System;
using Student.Types;

namespace Student.Public.Queries.Students
{
    public sealed class StudentView
    {
        /// <summary>
        /// Unique id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Public Unique id
        /// </summary>
        public String PublicId { get; }

        /// <summary>
        /// First name
        /// </summary>
        public String FirstName { get; }

        /// <summary>
        /// Last name
        /// </summary>
        public String LastName { get; }

        /// <summary>
        /// Second name
        /// </summary>
        public String SecondName { get; }

        /// <summary>
        /// User gender
        /// </summary>
        public StudentGender Gender { get; }

        public StudentView(Guid id, String publicId, String firstName, String lastName, String secondName, StudentGender gender)
            => (Id, PublicId, FirstName, LastName, SecondName, Gender)
                = (id, publicId, firstName, lastName, secondName, gender);
    }
}