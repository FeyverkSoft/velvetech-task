using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Query.Core;
using Student.Public.Queries.Students;
using Student.Types;

namespace Student.Public.Queries.Infrastructure.Students
{
    public sealed class StudentQueryHandler : IQueryHandler<StudentsQuery, Page<StudentView>>
    {
        private readonly StudentDbContext _context;

        public StudentQueryHandler(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<Page<StudentView>> Handle(StudentsQuery query, CancellationToken cancellationToken)
        {
            var iqSql = _context.Students
                .AsNoTracking()
                .Where(_ => _.MentorId == query.UserId &&
                            _.Status == StudentStatus.Active);

            if (!String.IsNullOrEmpty(query.Filter))
                iqSql = iqSql.Where(_ =>
                    EF.Functions.Like(_.LastName, $"%{query.Filter}%") ||
                    EF.Functions.Like(_.FirstName, $"%{query.Filter}%") ||
                    EF.Functions.Like(_.SecondName, $"%{query.Filter}%"));

            return new Page<StudentView>
            {
                Limit = query.Limit,
                Offset = query.Offset,
                Total = await iqSql.CountAsync(cancellationToken),
                Items = await iqSql
                    .OrderBy(p => p.CreateDate)
                    .Skip(query.Offset)
                    .Take(query.Limit)
                    .Select(_ => new StudentView(
                        _.Id,
                        _.PublicId,
                        _.FirstName,
                        _.LastName,
                        _.SecondName,
                        _.Gender
                    )).ToArrayAsync(cancellationToken)
            };
        }
    }
}