using System;
using System.Threading.Tasks;

namespace Student.Public.Domain.Users
{
    public interface IPasswordHasher
    {
        Task<String> HashPassword(String password);
    }
}
