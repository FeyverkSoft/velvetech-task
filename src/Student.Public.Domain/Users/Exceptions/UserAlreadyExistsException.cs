using System;

namespace Student.Public.Domain.Users.Exceptions
{
    public sealed class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string message = null) : base(message) { }
    }
}