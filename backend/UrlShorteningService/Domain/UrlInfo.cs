namespace UrlShorteningService.Domain
{
    public class UrlInfo : AuditableEntity<Guid>
    {
        public string ShortUrl { get; private set; }
        public string OriginalUrl { get; private set; }
        public Guid? UserId { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public int Clicks { get; private set; }

        private UrlInfo()
        {
            
        }

        public static UrlInfo Create(
            string originalUrl, 
            Guid? userId,
            string shortUrl,
            DateTime? expiryDate)
        {
            var url = new UrlInfo
            {
                Id = Guid.NewGuid(),
                ShortUrl = shortUrl,
                OriginalUrl = originalUrl,
                ExpiryDate = expiryDate.HasValue ? expiryDate.Value : DateTime.UtcNow.AddYears(1),
                Clicks = 0
            };

            url.UserId = userId;

            return url;
        }
    }
}
