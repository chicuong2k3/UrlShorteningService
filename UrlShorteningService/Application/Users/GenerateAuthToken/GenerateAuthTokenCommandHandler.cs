
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShorteningService.Application.Users.GenerateAuthToken
{
    internal sealed class GenerateAuthTokenCommandHandler(
        UserManager<AppUser> userManager,
        IConfiguration configuration)
        : ICommandHandler<GenerateAuthTokenCommand, AuthResponse>
    {
        public async Task<Result<AuthResponse>> Handle(GenerateAuthTokenCommand command, CancellationToken cancellationToken)
        {

            var user = await userManager.FindByNameAsync(command.Email);

            if (user == null)
            {
                return Result.Unauthorized();
            }
            var passwordValid = await userManager.CheckPasswordAsync(user, command.Password);

            if (!passwordValid)
            {
                return Result.Unauthorized();
            }

            return Result.Success(GenerateAuthResponse(user.Id, user.Email!));
        }

        private AuthResponse GenerateAuthResponse(string userId, string email)
        {
            var now = DateTime.UtcNow;
            var secret = configuration["JwtAuthSettings:Secret"]!;
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            };

            var jwtSecurityToken = new JwtSecurityToken(
                notBefore: now,
                claims: userClaims,
                expires: now.Add(TimeSpan.FromMinutes(60)),
                audience: configuration["JwtAuthSettings:Audience"]!,
                issuer: configuration["JwtAuthSettings:Issuer"]!,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            var result = new AuthResponse
            (
                userId,
                new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                string.Empty
            );

            return result;
        }
    }
}
