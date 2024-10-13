namespace ShorteningService.Application.Users.RegisterUser;

public sealed record RegisterUserResponse
(
    Guid UserId,
    string Email,
    string PhoneNumber
);