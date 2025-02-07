using System.Diagnostics.CodeAnalysis;
using WebForum.Api.Filters;
using WebForum.Api.Services;
using WebForum.Application.Common.Interfaces;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.AddControllers(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        services.ConfigureHealthChecks(config);

        services.ConfigureAuth(config);

        services.ConfigureFluentValidator();

        services.ConfigureAutoMapper();

        services.ConfigureSwagger(config);

        services.ConfigureSettings(config);

        return services;
    }
}