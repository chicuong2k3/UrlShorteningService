namespace UrlShorteningService.Presentation.ShortLinks;

public record CreateShortUrlRequest
(
    string LongUrl,
    string ApiKey,
    string? CustomDomain = null,
    DateTime? ExpiryDate = null
);
