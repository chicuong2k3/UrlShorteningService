namespace ShorteningService.Application.Users.GenerateAuthToken
{
    internal class GenerateAuthTokenCommandValidator : AbstractValidator<GenerateAuthTokenCommand>
    {
        public GenerateAuthTokenCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}
