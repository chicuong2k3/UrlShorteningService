namespace UrlShorteningService.Application.ShortLinks.Create;

public sealed record CreateShortUrlResponse
(
    string Short,
    string Original,
    DateTime CreatedAt,
    bool IsActive
);