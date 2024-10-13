namespace ShorteningService.Application.Users.GenerateAuthToken;

public record GenerateAuthTokenCommand
(
    string Email,
    string Password
) : ICommand<AuthResponse>;