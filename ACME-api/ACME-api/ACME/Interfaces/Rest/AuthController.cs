using ACME_api.ACME.Application.Internal.CommandServices;
using ACME_api.ACME.Domain.Model.Commands.User;
using ACME_api.ACME.Domain.Services.Auth;
using ACME_api.ACME.Interfaces.Rest.Resources.Auth;
using ACME_api.ACME.Interfaces.Rest.Transform.Auth;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace ACME_api.ACME.Interfaces.Rest
{
    [ApiController]
    [Route("api/v1/auth")]
    [Produces(MediaTypeNames.Application.Json)]
    [Tags("authentication")]
    public class AuthController(IAuthCommandService _authCommandService) : ControllerBase
    {
        [HttpPost("signup")]
        [SwaggerOperation(
        Summary = "Registers a new user",
        Description = "Creates a user and returns authentication info",
        OperationId = "SignUp")]
        [SwaggerResponse(201, "User created successfully", typeof(SignUpResponseResource))]
        [SwaggerResponse(400, "Invalid request")]
        public async Task<ActionResult> SignUp([FromBody] SignUpRequestResource resource)
        {
            var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
            try
            {
                var result = await _authCommandService.SignUp(signUpCommand);
                return Created(string.Empty, result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("signin")]
        [SwaggerOperation(
        Summary = "Authenticates a user",
        Description = "Validates credentials and returns a JWT token",
        OperationId = "SignIn")]
        [SwaggerResponse(200, "Login successful", typeof(SignInResponseResource))]
        [SwaggerResponse(400, "Invalid credentials")]
        public async Task<ActionResult> SignIn([FromBody] SignInRequestResource resource)
        {
            try
            {
                var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
                var result = await _authCommandService.SignIn(signInCommand);

                return Ok(result);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
