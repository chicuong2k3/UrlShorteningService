namespace UrlShorteningService.Application.ShortLinks.Create;

public record CreateShortUrlCommand
(
    string LongUrl,
    string ApiKey,
    Guid? UserId,
    string? CustomDomain,
    DateTime? ExpiryDate
) : ICommand<CreateShortUrlResponse>;