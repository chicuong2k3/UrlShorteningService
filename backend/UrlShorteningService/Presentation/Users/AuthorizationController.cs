using ShorteningService.Application.Users.GenerateAuthToken;

namespace ShorteningService.Presentation.Users
{
    [ApiController]
    //[Authorize(Policy = nameof(AuthorizationPolicies.IsCustomer))]
    public class AuthorizationController : ControllerBase
    {
        private readonly ISender _sender;

        public AuthorizationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("/authorization/token")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request)
        {
            var response = await _sender.Send(new GenerateAuthTokenCommand(
                    request.Email,
                    request.Password
                ));
            return this.ToActionResult(response);
        }

    }
}
