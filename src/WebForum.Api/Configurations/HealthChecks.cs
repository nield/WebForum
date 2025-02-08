using System.Diagnostics.CodeAnalysis;
using WebForum.Infrastructure.Persistence;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class HealthChecks
{
    public static void ConfigureHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("Database connectivity");
    }
}