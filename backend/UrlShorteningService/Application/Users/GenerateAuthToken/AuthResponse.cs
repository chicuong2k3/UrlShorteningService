namespace ShorteningService.Application.Users.GenerateAuthToken;

public sealed record AuthResponse
(
    Guid UserId,
    string AccessToken
);