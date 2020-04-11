using System;

namespace Student.Public.Queries.Students
{
    public sealed class StudentsQuery : PageQuery<StudentView>
    {
        public Guid UserId { get; }
        public String Filter { get; }

        public StudentsQuery(Int32 offset, Int32 limit, Guid userId, String filter) : base(offset, limit)
            => (UserId, Filter)
                = (userId, filter?.Trim());
    }
}