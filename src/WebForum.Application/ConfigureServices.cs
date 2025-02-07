using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WebForum.Application.Common.Behaviours;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            options.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        return services;
    }
}