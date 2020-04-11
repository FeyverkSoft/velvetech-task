using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Student.Public.Domain.Students;

namespace Student.Public.Infrastructure.Students
{
    public sealed class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Students.Entity.Student> Get(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Students.SingleOrDefaultAsync(_ => _.Id == id, cancellationToken);
        }

        public async Task<Domain.Students.Entity.Student> GetByPublicApi(String id, CancellationToken cancellationToken)
        {
            return await _context.Students.SingleOrDefaultAsync(_ => _.PublicId == id, cancellationToken);
        }

        public async Task Save(Domain.Students.Entity.Student student, CancellationToken cancellationToken)
        {
            var entry = _context.Entry(student);
            if (entry.State == EntityState.Detached)
                _context.Students.Add(student);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}