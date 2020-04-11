using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Query.Core;
using Student.Public.Domain.Students;
using Student.Public.Domain.Students.Exceptions;
using Student.Public.Queries;
using Student.Public.Queries.Students;
using Student.Public.WebApi.Extensions;
using Student.Public.WebApi.Models.Students;

namespace Student.Public.WebApi.Controllers
{
    [ApiController]
    [ProducesResponseType(401)]
    public sealed class StudentsController : ControllerBase
    {
        /// <summary>
        /// Add new student
        /// </summary>
        /// <param name="binding">student model</param>
        /// <response code="204">Successfully</response>
        /// <response code="409">student already registered with other params</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(409)]
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
                return Conflict();
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
            IQueryProcessor processor,
            [FromBody] StudentsQueryBinding binding
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
    }
}