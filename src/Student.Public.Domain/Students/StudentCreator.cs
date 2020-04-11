using System;
using System.Threading;
using System.Threading.Tasks;
using Student.Public.Domain.Students.Exceptions;
using Student.Types;

namespace Student.Public.Domain.Students
{
    public sealed class StudentCreator
    {
        private readonly IStudentRepository _repository;

        public StudentCreator(IStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Entity.Student> Create(Guid id, Guid mentorId, String publicId, String firstName, String lastName,
            String secondName, StudentGender gender, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentException("Incorrect firstName", nameof(firstName));

            if (String.IsNullOrEmpty(firstName))
                throw new ArgumentException("Incorrect lastName", nameof(lastName));

            if (!String.IsNullOrEmpty(publicId) && (publicId.Length < 6 || publicId.Length > 16))
                throw new ArgumentException("Incorrect publicId", nameof(publicId));

            var existsStudent = await _repository.Get(id, mentorId, cancellationToken);

            if (existsStudent == null && !String.IsNullOrEmpty(publicId))
                existsStudent = await _repository.GetByPublicId(publicId, cancellationToken);

            if (existsStudent != null)
                if (existsStudent.Id == id &&
                    existsStudent.FirstName == firstName &&
                    existsStudent.LastName == lastName &&
                    existsStudent.SecondName == secondName &&
                    existsStudent.Gender == gender &&
                    existsStudent.PublicId == publicId &&
                    existsStudent.MentorId == mentorId)
                    return existsStudent;
                else
                    throw new StudentAlreadyExistsException();

            return new Entity.Student(id, mentorId, publicId, firstName, lastName, secondName, gender);
        }
    }
}