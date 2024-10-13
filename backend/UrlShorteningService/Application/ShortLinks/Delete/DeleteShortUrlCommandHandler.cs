namespace UrlShorteningService.Application.ShortLinks.Delete
{
    internal class DeleteShortUrlCommandHandler(AppDbContext dbContext)
        : ICommandHandler<DeleteShortUrlCommand>
    {
        public async Task<Result> Handle(DeleteShortUrlCommand command, CancellationToken cancellationToken)
        {
            var urlInfo = await dbContext.UrlInfos.FindAsync(command.Id);

            if (urlInfo == null)
            {
                return Result.NotFound();
            }

            dbContext.UrlInfos.Remove(urlInfo);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Result.NoContent();
        }
    }
}
