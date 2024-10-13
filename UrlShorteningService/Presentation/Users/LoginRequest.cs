namespace ShorteningService.Presentation.Users;

public sealed record LoginRequest
(
    string Email,
    string Password
);
