namespace WebForum.Application.Common.Interfaces.Repositories;

public interface ILikeRepository : IRepository<Like>
{
    Task<Like> GetLikeAsync(long postId, string userId, CancellationToken cancellationToken);
}
