namespace ShorteningService.Application.Users.RegisterUser
{
    internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^(0|\+84)\d{9}$");
        }
    }
}
