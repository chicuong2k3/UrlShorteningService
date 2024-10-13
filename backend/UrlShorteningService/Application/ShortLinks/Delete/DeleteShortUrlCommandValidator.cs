namespace UrlShorteningService.Application.ShortLinks.Delete
{
    internal class DeleteShortUrlCommandValidator : AbstractValidator<DeleteShortUrlCommand>
    {
        public DeleteShortUrlCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
