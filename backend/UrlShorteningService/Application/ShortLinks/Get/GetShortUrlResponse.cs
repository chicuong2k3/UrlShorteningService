namespace UrlShorteningService.Application.ShortLinks.Get;

public record GetShortUrlResponse
(
    Guid? Id,
    string Original,
    string Short,
    DateTime CreatedAt,
    DateTime? ExpiredAt
);
