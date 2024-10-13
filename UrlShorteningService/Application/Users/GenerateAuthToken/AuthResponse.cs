namespace ShorteningService.Application.Users.GenerateAuthToken;

public sealed record AuthResponse
(
    string UserId,
    string AccessToken,
    string RefreshToken
);