using System;

namespace Student.Public.WebApi.Exceptions
{
    public static class ErrorCodes
    {
        public const String InternalServerError = "internal_server_error";
        public const String PublicIdAlreadyExists = "publicId_already_exists";
        public const String StudentNotFound = "student_not_found";
        public const String StudentAlreadyExists = "student_already_exists";
        public const String UserAlreadyExists = "user_already_exists";
    }
}
