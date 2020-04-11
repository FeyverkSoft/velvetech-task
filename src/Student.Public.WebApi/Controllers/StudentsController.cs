using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Query.Core;
using Student.Public.Domain.Students;
using Student.Public.Domain.Students.Exceptions;
using Student.Public.Queries;
using Student.Public.Queries.Students;
using Student.Public.WebApi.Exceptions;
using Student.Public.WebApi.Extensions;
using Student.Public.WebApi.Models.Students;

namespace Student.Public.WebApi.Controllers
{
    [ApiController]
    [ProducesResponseType(401)]
    [Authorize]
    public sealed class StudentsController : ControllerBase
    {
        /// <summary>
        /// Add new student
        /// </summary>
        /// <param name="binding">student model</param>
        /// <response code="204">Successfully</response>
        /// <response code="409">student already registered with other params</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails), 409)]
        [HttpPost("/students")]
        public async Task<IActionResult> AddStudent(
            CancellationToken cancellationToken,
            [FromBody] StudentAddBinding binding,
            [FromServices] IStudentRepository repository,
            [FromServices] StudentCreator registrar)
        {
            try{
                var student = await registrar.Create(
                    id: binding.Id,
                    mentorId: User.GetUserId(),
                    publicId: binding.PublicId,
                    firstName: binding.FirstName,
                    lastName: binding.LastName,
                    secondName: binding.SecondName,
                    gender: binding.Gender,
                    cancellationToken);
                await repository.Save(student, cancellationToken);
                return NoContent();
            }
            catch (StudentAlreadyExistsException){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.StudentAlreadyExists, "Student already exists");
            }
        }

        /// <summary>
        /// Get student list
        /// </summary>
        /// <param name="binding">student list query model</param>
        /// <response code="200">Ok</response>
        [ProducesResponseType(typeof(Page<StudentView>), 200)]
        [HttpGet("/students")]
        public async Task<IActionResult> GetStudents(
            CancellationToken cancellationToken,
            [FromServices] IQueryProcessor processor,
            [FromQuery] StudentsQueryBinding binding
        )
        {
            return Ok(await processor.Process<StudentsQuery, Page<StudentView>>(new StudentsQuery(
                    offset: binding.Offset,
                    limit: binding.Limit,
                    userId: User.GetUserId(),
                    filter: binding.Filter
                ),
                cancellationToken));
        }

        /// <summary>
        /// Delete student
        /// </summary>
        /// <param name="id">student id</param>
        /// <response code="204">Ok</response>
        /// <response code="404">not found</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),404)]
        [HttpDelete("/students/{id}")]
        public async Task<IActionResult> DeleteStudent(
            CancellationToken cancellationToken,
            [FromServices] IStudentRepository repository,
            [FromRoute] Guid id
        )
        {
            var student = await repository.Get(id, User.GetUserId(), cancellationToken);
            if (student == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.StudentNotFound, "Student not found");

            student.MarkAsDeleted();
            await repository.Save(student, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Update student
        /// </summary>
        /// <param name="id">student id</param>
        /// <response code="204">Ok</response>
        /// <response code="404">not found</response>
        /// <response code="409">conflict</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ProblemDetails),409)]
        [ProducesResponseType(typeof(ProblemDetails),404)]
        [HttpPost("/students/{id}")]
        public async Task<IActionResult> UpdateStudent(
            CancellationToken cancellationToken,
            [FromServices] IStudentRepository repository,
            [FromRoute] Guid id,
            [FromBody] StudentUpdateBinding binding
        )
        {
            var student = await repository.Get(id, User.GetUserId(), cancellationToken);
            if (student == null)
                throw new ApiException(HttpStatusCode.NotFound, ErrorCodes.StudentNotFound, "Student not found");
            try{
                student.Update(binding.FirstName, binding.LastName, binding.SecondName, binding.Gender, binding.PublicId);
                await repository.Save(student, cancellationToken);
                return NoContent();
            }
            catch (DbUpdateException ex) when (ex.InnerException?.Message?.Contains("Duplicate") == true){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.PublicIdAlreadyExists, "PublicId already exists");
            }
        }
    }
}