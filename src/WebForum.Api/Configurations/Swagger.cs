using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class Swagger
{
    public static void ConfigureSwagger(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    public static void UseApiDocumentation(this WebApplication app, IConfiguration configuration)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}