namespace WebForum.Infrastructure.Persistence.Repositories;

public class CommentRepository : BaseRepository<Comment>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
