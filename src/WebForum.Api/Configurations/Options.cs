using System.Diagnostics.CodeAnalysis;
using WebForum.Application.Common.Settings;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class Options
{
    public static void ConfigureSettings(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppSettings>(config);
    }
}