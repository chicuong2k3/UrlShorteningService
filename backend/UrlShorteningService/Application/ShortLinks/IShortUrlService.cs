namespace UrlShorteningService.Application.ShortLinks
{
    public interface IShortUrlService
    {
        string GenerateShortUrl(string shortUrl, string? customDomain);
        string ConvertToOriginalUrl(string longUrl, string? customDomain);
    }
}
