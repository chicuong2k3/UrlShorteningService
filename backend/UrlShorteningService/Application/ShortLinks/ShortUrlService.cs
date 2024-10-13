using System.Security.Cryptography;
using System.Text;

namespace UrlShorteningService.Application.ShortLinks
{
    public class ShortUrlService : IShortUrlService
    {
        private const string DefaultDomain = "https://short.vn";

        public string ConvertToOriginalUrl(string shortUrl, string? customDomain)
        {
            var domain = string.IsNullOrEmpty(customDomain) ? DefaultDomain : customDomain.TrimEnd('/');
            var encodedUrl = shortUrl.Replace($"{domain}/", "");
            var base64 = encodedUrl.Replace('-', '+').Replace('_', '/');
            switch (encodedUrl.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        public string GenerateShortUrl(string longUrl, string? customDomain)
        {
            using (var md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(longUrl);
                var hashBytes = md5.ComputeHash(bytes);
                var shortUrl = Convert.ToBase64String(hashBytes)
                    .Replace('+', '-')
                    .Replace('/', '_')
                    .TrimEnd('=')
                    .Substring(0, 7); // Ensure it's 7 characters long

                var domain = string.IsNullOrEmpty(customDomain) ? DefaultDomain : customDomain.TrimEnd('/');
                return $"{domain}/{shortUrl}";
            }
        }
    }
}
