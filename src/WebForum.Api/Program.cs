using WebForum.Api.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Diagnostics.CodeAnalysis;
using WebForum.Api.Extension;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureLogging();

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsStaging() || app.Environment.IsProduction())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (!app.Environment.IsProduction())
{
    app.UseApiDocumentation(app.Configuration);
}

app.UseLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapGet("/ping", () => "Working as expected");

app.MapIdentityApi();

await app.ApplyMigrations();

await app.RunAsync();

// Make the implicit Program class public so test projects can access it
[ExcludeFromCodeCoverage]
public partial class Program
{
    protected Program()
    {
    }
}