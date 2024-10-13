namespace ShorteningService.Application.Users.RegisterUser;

public sealed record RegisterUserResponse
(
    string UserId,
    string Email,
    string PhoneNumber
);