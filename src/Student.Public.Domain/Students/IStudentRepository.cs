using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Public.Domain.Students
{
    public interface IStudentRepository
    {
        public Task<Entity.Student> Get(Guid id, Guid mentorId, CancellationToken cancellationToken);
        public Task<Entity.Student> GetByPublicId(String id, CancellationToken cancellationToken);

        public Task Save(Entity.Student student, CancellationToken cancellationToken);
    }
}
