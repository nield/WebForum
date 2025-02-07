using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using WebForum.Application.Common.Interfaces;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class AutoMapper
{
    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(IApplicationMarker).Assembly);
    }
}