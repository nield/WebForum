using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace WebForum.Api.Configurations;

[ExcludeFromCodeCoverage]
public static class FluentValidator
{
    public static void ConfigureFluentValidator(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblies(new[]
        {
            Assembly.GetExecutingAssembly()
        })
        .AddFluentValidationAutoValidation();
    }
}