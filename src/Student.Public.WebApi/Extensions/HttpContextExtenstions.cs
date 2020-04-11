using System;
using System.Security.Claims;

namespace Student.Public.WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        /// <summary>
        /// get authorized userId
        /// </summary>
        /// <returns></returns>
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            return new Guid(user.FindFirst(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);
        }
    }
}
