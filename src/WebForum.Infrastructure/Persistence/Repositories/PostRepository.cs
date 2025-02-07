namespace WebForum.Infrastructure.Persistence.Repositories;

public class PostRepository : BaseRepository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {

    }
}
