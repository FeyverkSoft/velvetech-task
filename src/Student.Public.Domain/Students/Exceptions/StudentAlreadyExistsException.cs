using System;

namespace Student.Public.Domain.Students.Exceptions
{
    public sealed class StudentAlreadyExistsException : Exception
    {
        public StudentAlreadyExistsException(String message = null) : base(message) { }
    }
}
