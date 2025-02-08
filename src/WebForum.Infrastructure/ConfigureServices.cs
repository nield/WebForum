using System.Diagnostics.CodeAnalysis;
using WebForum.Infrastructure.Common;
using WebForum.Infrastructure.Persistence;
using WebForum.Infrastructure.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using WebForum.Infrastructure.Services;

namespace Microsoft.Extensions.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.SetupIdentity();
        services.SetupServices();
        services.SetupDatabase(configuration);
        services.SetupCaching(configuration);
        services.SetupRepositories();

        return services;
    }

    private static void SetupServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityService, IdentityService>();
    }

    private static void SetupDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                                    .EnableRetryOnFailure(maxRetryCount: 3));
        });
    }

    private static void SetupIdentity(this IServiceCollection services)
    {
        services.AddIdentityApiEndpoints<User>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;

            options.SignIn.RequireConfirmedEmail = false;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>() 
        .AddDefaultTokenProviders()        
        .AddApiEndpoints();
    }

    private static void SetupRepositories(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblyOf<IInfrastructureMarker>()
                                    .AddClasses(c => c.AssignableTo(typeof(IRepository<>)))
                                    .AsImplementedInterfaces()
                                    .WithScopedLifetime());
    }

    private static void SetupCaching(this IServiceCollection services,
        IConfiguration configuration)
    {
        if (!string.IsNullOrEmpty(configuration["RedisOptions:ConnectionString"]))
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["RedisOptions:ConnectionString"];
                options.InstanceName = configuration["RedisOptions:InstanceName"];
            });
        }
        else
        {
            services.AddDistributedMemoryCache();
        }
    }
}