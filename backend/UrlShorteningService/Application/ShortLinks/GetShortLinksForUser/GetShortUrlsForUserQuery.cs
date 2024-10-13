namespace UrlShorteningService.Application.ShortLinks.GetShortLinksForUser;

public record GetShortUrlsForUserQuery
(
    Guid UserId,
    int PageNumber,
    int PageSize,
    string? OrderBy
) : IQuery<PagedResult<IEnumerable<ShortUrlDto>>>;
