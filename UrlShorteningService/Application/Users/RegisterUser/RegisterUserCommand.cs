namespace ShorteningService.Application.Users.RegisterUser;

public record RegisterUserCommand
(
    string Email,
    string Password,
    string PhoneNumber
) : ICommand<RegisterUserResponse>;