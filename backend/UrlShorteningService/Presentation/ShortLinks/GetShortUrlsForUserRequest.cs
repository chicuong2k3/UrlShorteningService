namespace UrlShorteningService.Presentation.ShortLinks;

public record GetShortUrlsForUserRequest
(
    int PageNumber,
    int PageSize,
    string? OrderBy
);