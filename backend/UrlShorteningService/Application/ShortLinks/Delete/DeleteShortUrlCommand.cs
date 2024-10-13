namespace UrlShorteningService.Application.ShortLinks.Delete;

public record DeleteShortUrlCommand(Guid Id) : ICommand;
