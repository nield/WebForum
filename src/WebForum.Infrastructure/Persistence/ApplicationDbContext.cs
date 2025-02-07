using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace WebForum.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    #region DbSets

    public DbSet<Post> Posts => Set<Post>();

    #endregion DbSets

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}