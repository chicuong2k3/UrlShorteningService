namespace UrlShorteningService.Application.ShortLinks.GetShortLinksForUser;

public record ShortUrlDto
(
    Guid? Id,
    string Original,
    string Short,
    DateTime CreatedAt,
    bool IsActive
);
