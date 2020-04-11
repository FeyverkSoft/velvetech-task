using System;
using System.Threading;
using System.Threading.Tasks;

namespace Student.Public.Domain.Students
{
    public interface IStudentRepository
    {
        public Task<Entity.Student> Get(Guid id, CancellationToken cancellationToken);
        public Task<Entity.Student> GetByPublicApi(String id, CancellationToken cancellationToken);

        public Task Save(Entity.Student student, CancellationToken cancellationToken);
    }
}
