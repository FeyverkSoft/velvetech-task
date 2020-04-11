using System;

namespace Student.Public.Domain.Users.Exceptions
{
    public sealed class WrongPasswordException : Exception
    {
        public WrongPasswordException(String message):base(message) { }
    }
}
