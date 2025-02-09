using Microsoft.EntityFrameworkCore;

namespace WebForum.Infrastructure.Tests.Persistence.Interceptors;

public class FakeEntityDbContext : DbContext
{
    public DbSet<FakeEntity> FakeEntities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        base.OnConfiguring(optionsBuilder);
    }
}
