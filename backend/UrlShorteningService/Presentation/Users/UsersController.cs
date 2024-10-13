using ShorteningService.Application.Users.DeleteUser;
using ShorteningService.Application.Users.RegisterUser;

namespace ShorteningService.Presentation.Users
{
    [ApiController]
    //[Authorize(Policy = nameof(AuthorizationPolicies.IsCustomer))]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("/users")]
        public async Task<ActionResult<RegisterUserResponse>> RegisterUser([FromBody] RegisterUserCommand request)
        {
            var response = await _sender.Send(request);
            return this.ToActionResult(response);
        }

        
        [HttpDelete("/users/{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand(id);
            var response = await _sender.Send(command);
            return this.ToActionResult(response);
        }

    }
}
