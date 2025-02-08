namespace WebForum.Infrastructure.Persistence.Repositories;

public class LikeRepository : BaseRepository<Like>, ILikeRepository
{
    public LikeRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Like> GetLikeAsync(long postId, string userId, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(x => 
            x.PostId == postId && x.CreatedBy == userId, 
            cancellationToken);
    }
}
