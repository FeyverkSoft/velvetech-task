using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.Public.Domain.Users;
using Student.Public.Domain.Users.Exceptions;
using Student.Public.WebApi.Exceptions;
using Student.Public.WebApi.Models.Registrations;

namespace Student.Public.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    public sealed class RegistrationController : ControllerBase
    {
        /// <summary>
        /// Register user
        /// </summary>
        /// <param name="binding">Registration model</param>
        /// <response code="204">Successfully</response>
        /// <response code="400">Invalid registration parameters format</response>
        /// <response code="409">User already registered</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(412)]
        [HttpPost("/registrations")]
        public async Task<IActionResult> Registration(
            CancellationToken cancellationToken,
            [FromBody] RegistrationBinding binding,
            [FromServices] IUserRepository repository,
            [FromServices] UserRegistrar registrar)
        {
            try{
                var user = await registrar.Registrate(binding.Login, binding.Name, binding.Password, cancellationToken);
                await repository.Save(user, cancellationToken);

                return NoContent();
            }
            catch (UserAlreadyExistsException){
                throw new ApiException(HttpStatusCode.Conflict, ErrorCodes.UserAlreadyExists, "User already exists");
            }
        }
    }
}