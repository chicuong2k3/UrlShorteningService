using UrlShorteningService.Domain;

namespace UrlShorteningService.Application.ShortLinks.Create
{
    internal sealed class CreateShortUrlCommandHandler(
        AppDbContext dbContext,
        IShortUrlService shortLinkService,
        UserManager<AppUser> userManager)
        : ICommandHandler<CreateShortUrlCommand, CreateShortUrlResponse>
    {
        public async Task<Result<CreateShortUrlResponse>> Handle(CreateShortUrlCommand command, CancellationToken cancellationToken)
        {
            AppUser? user = null;
            if (command.UserId.HasValue)
            {
                user = await userManager.FindByIdAsync(command.UserId.Value.ToString());

                if (user == null)
                {
                    return Result.Unauthorized();
                }
            }

            var urlInfo = await dbContext.UrlInfos
                                    .Where(x => 
                                    x.OriginalUrl.Equals(command.LongUrl)
                                    && x.UserId == command.UserId)
                                    .FirstOrDefaultAsync();

            if (urlInfo == null)
            {
                var shortUrl = shortLinkService.GenerateShortUrl(command.LongUrl, command.CustomDomain);

                urlInfo = UrlInfo.Create
                (
                    command.LongUrl,
                    command.UserId,
                    shortUrl,
                    command.ExpiryDate
                );

                dbContext.UrlInfos.Add(urlInfo);
                await dbContext.SaveChangesAsync(cancellationToken);
            }

            
            return Result<CreateShortUrlResponse>.Success(
                new CreateShortUrlResponse(
                    urlInfo.ShortUrl,
                    urlInfo.OriginalUrl,
                    urlInfo.CreatedAt,
                    urlInfo.ExpiryDate > DateTime.UtcNow)
            );
        }
    }
}
