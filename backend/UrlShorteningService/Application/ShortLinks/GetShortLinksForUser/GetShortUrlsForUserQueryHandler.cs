using System.Linq.Dynamic.Core;

namespace UrlShorteningService.Application.ShortLinks.GetShortLinksForUser
{
    internal sealed class GetShortUrlsForUserQueryHandler(
        AppDbContext dbContext,
        UserManager<AppUser> userManager)
        : IQueryHandler<GetShortUrlsForUserQuery, Ardalis.Result.PagedResult<IEnumerable<ShortUrlDto>>>
    {
        public async Task<Result<Ardalis.Result.PagedResult<IEnumerable<ShortUrlDto>>>> Handle(GetShortUrlsForUserQuery query, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(query.UserId.ToString());

            if (user == null)
            {
                return Result.NotFound("User not found.");
            }

            var urlInfos = dbContext.UrlInfos
                                .Where(x => x.UserId == query.UserId);

            var count = await urlInfos.CountAsync();

            if (!string.IsNullOrWhiteSpace(query.OrderBy))
            {
                urlInfos = urlInfos.OrderBy(query.OrderBy);
            }

            var result = await urlInfos.Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .ToListAsync(cancellationToken);


            var totalPages = (int)Math.Ceiling(count / (double)query.PageSize);

            return Result.Success(
                new Ardalis.Result.PagedResult<IEnumerable<ShortUrlDto>>(
                    new PagedInfo(
                        query.PageNumber,
                        query.PageSize,
                        totalPages,
                        count
                    ),
                    result.Select(x => new ShortUrlDto(
                            x.Id, 
                            x.OriginalUrl, 
                            x.ShortUrl, 
                            x.CreatedAt,
                            x.ExpiryDate > DateTime.Now))
                )    
            );
        }
    }
}
