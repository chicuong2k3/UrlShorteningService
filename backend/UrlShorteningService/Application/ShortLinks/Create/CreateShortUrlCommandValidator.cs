using System;

namespace UrlShorteningService.Application.ShortLinks.Create
{
    internal class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
    {
        public CreateShortUrlCommandValidator()
        {
            RuleFor(x => x.LongUrl)
            .NotEmpty()
                .Must(url => IsValidUrl(url));

            RuleFor(x => x.ApiKey)
                .NotEmpty();
        }

        private bool IsValidUrl(string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var result) &&
             (result.Scheme == Uri.UriSchemeHttp || 
                result.Scheme == Uri.UriSchemeHttps);
        }
    }
}
