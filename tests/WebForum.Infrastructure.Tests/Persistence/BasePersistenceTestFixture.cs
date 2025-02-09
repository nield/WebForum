using WebForum.Infrastructure.Persistence;

namespace WebForum.Infrastructure.Tests.Persistence;

public abstract class BasePersistenceTestFixture<T>
    : BaseTestFixture<T> where T : class
{
    protected ApplicationDbContext _dbContext;

    protected BasePersistenceTestFixture()
    {
        _dbContext = InMemoryApplicationDbContextFactory.CreateContext(Guid.NewGuid().ToString());
    }
}
