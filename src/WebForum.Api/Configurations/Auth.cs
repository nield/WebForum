using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class Auth
{
    public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();

        //services.AddAuthentication()
        //    .AddBearerToken(IdentityConstants.BearerScheme);
    }
}