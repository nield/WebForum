using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace WebForum.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    public const string MigrationTableName = "__EFMigrationsHistory";

    #region DbSets

    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Comment> Comments => Set<Comment>();

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