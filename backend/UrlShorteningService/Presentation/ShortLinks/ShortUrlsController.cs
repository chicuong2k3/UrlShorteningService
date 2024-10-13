using UrlShorteningService.Application.ShortLinks.Create;
using UrlShorteningService.Application.ShortLinks.Delete;
using UrlShorteningService.Application.ShortLinks.Get;
using UrlShorteningService.Application.ShortLinks.GetShortLinksForUser;

namespace UrlShorteningService.Presentation.ShortLinks
{
    [ApiController]
    //[Authorize(Policy = nameof(AuthorizationPolicies.IsCustomer))]
    public class ShortUrlsController : ControllerBase
    {
        private readonly ISender _sender;

        public ShortUrlsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPut("/urls")]
        public async Task<ActionResult<CreateShortUrlResponse>> Create([FromBody] CreateShortUrlRequest request)
        {
            var response = await _sender.Send(new CreateShortUrlCommand(
                request.LongUrl,
                request.ApiKey,
                User.GetUserId(),
                request.CustomDomain,
                request.ExpiryDate
            ));

            return this.ToActionResult(response);
        }


        [HttpDelete("/urls/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteShortUrlCommand(id);
            var response = await _sender.Send(command);
            return this.ToActionResult(response);
        }

        [HttpGet("/urls/{id}")]
        public async Task<ActionResult<GetShortUrlResponse>> Get(Guid id)
        {
            var query = new GetShortUrlQuery(id);
            var response = await _sender.Send(query);
            return this.ToActionResult(response);
        }

        [HttpGet("/urls")]
        public async Task<ActionResult<PagedResult<IEnumerable<ShortUrlDto>>>> GetShortLinksForUser(
            [FromQuery] GetShortUrlsForUserRequest request)
        {
            var query = new GetShortUrlsForUserQuery(
                User.GetUserId(),
                request.PageNumber, 
                request.PageSize, 
                request.OrderBy);

            var response = await _sender.Send(query);

            return this.ToActionResult(response);
        }
    }
}
