namespace UrlShorteningService.Application.ShortLinks.Get;

public record GetShortUrlQuery
(
    Guid Id
) : IQuery<GetShortUrlResponse>;
