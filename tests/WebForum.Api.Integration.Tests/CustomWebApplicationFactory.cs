using WebForum.Api.Integration.Tests.Mocks;
using WebForum.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using WebForum.Api.Integration.Tests.Containers;
using Microsoft.Extensions.Hosting;
using WebForum.Application.Common.Constants;
using System.Diagnostics.CodeAnalysis;

namespace WebForum.Api.Integration.Tests;

[ExcludeFromCodeCoverage]
public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{
    public string DefaultUserId { get; set; } = UserConstants.IntegrationTestUserId;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable(
            "DOTNET_HOSTBUILDER__RELOADCONFIGONCHANGE",
            "false"
        );

        Environment.SetEnvironmentVariable(
            "ConnectionStrings__DefaultConnection",
            DatabaseContainer.Instance.GetConnectionString()
        );

        Environment.SetEnvironmentVariable(
            "IdentitySettings__StandardPassword",
            "St@nd@rd123!");

        builder.UseEnvironment(EnvironmentConstants.Test);

        builder.ConfigureTestServices(services =>
        {
            services.Configure<TestAuthHandlerOptions>(
                options => options.DefaultUserId = DefaultUserId
            );

            services
                .AddAuthentication(TestAuthHandler.AuthenticationScheme)
                .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(
                    TestAuthHandler.AuthenticationScheme,
                    options => { }
                );

            services.AddScoped<ICurrentUserService, MockCurrentUserService>();
        });

        base.ConfigureWebHost(builder);
    }
}
