using System.Security.Claims;

namespace ShorteningService.Presentation.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal claims)
        {
            return new Guid("fe8e234e-3fb1-4fa5-abc6-8e6aafc98a91");
            //return Guid.TryParse(claims.FindFirstValue(ClaimTypes.NameIdentifier), out var userId)
            //    ? userId : throw new Exception("Invalid user.");
        }
    }
}
