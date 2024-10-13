namespace UrlShorteningService.Application.ShortLinks.Get
{
    internal sealed class GetShortUrlQueryHandler(AppDbContext dbContext)
        : IQueryHandler<GetShortUrlQuery, GetShortUrlResponse>
    {
        public async Task<Result<GetShortUrlResponse>> Handle(GetShortUrlQuery query, CancellationToken cancellationToken)
        {
            var urlInfo = await dbContext.UrlInfos.FindAsync(query.Id);

            if (urlInfo == null)
            {
                return Result<GetShortUrlResponse>.NotFound();
            }

            return Result<GetShortUrlResponse>.Success(new GetShortUrlResponse
            (
                urlInfo.Id,
                urlInfo.OriginalUrl,
                urlInfo.ShortUrl,
                urlInfo.CreatedAt,
                urlInfo.ExpiryDate
            ));
        }
    }
}
