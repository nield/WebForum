using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebForum.Application.Common.Constants;
using WebForum.Application.Common.Models;
using WebForum.Application.Common.Settings;

namespace WebForum.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly IIdentityService _identityService;
    private readonly IOptions<AppSettings> _appSettings;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<User> userManager,
        IIdentityService identityService,
        IOptions<AppSettings> appSettings,
        ILogger<ApplicationDbContextInitialiser> logger
    )
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _identityService = identityService;
        _appSettings = appSettings;
    }

    public async Task MigrateDatabaseAsync()
    {
        try
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }

    public async Task SeedDataAsync()
    {
        try
        {
            // Ensure existing seed data is saved before adding new data.
            await _context.Database.EnsureCreatedAsync();

            await SeedRolesAsync();

            await SeedUsersAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private async Task SeedUsersAsync()
    {
        var moderatorUser = await _userManager.FindByNameAsync(UserConstants.ModeratorUsername);

        if (moderatorUser is null)
        {
            await _identityService.RegisterUser(new RegisterUserDto
            {
                Email = UserConstants.ModeratorUsername,
                Name = "Moderator",
                Surname = "Moderator",
                RoleName = RoleConstants.Moderator,
                Password = _appSettings.Value.IdentitySettings.ModeratorPassword
            });
        }

        var standardUser1 = await _userManager.FindByNameAsync(UserConstants.StandardUsername1);

        if (standardUser1 is null)
        {
            await _identityService.RegisterUser(new RegisterUserDto
            {
                Email = UserConstants.StandardUsername1,
                Name = "John",
                Surname = "Doe",
                RoleName = RoleConstants.Standard,
                Password = _appSettings.Value.IdentitySettings.StandardPassword
            });
        }

        var standardUser2 = await _userManager.FindByNameAsync(UserConstants.StandardUsername2);

        if (standardUser2 is null)
        {
            await _identityService.RegisterUser(new RegisterUserDto
            {
                Email = UserConstants.StandardUsername2,
                Name = "Jane",
                Surname = "Doe",
                RoleName = RoleConstants.Standard,
                Password = _appSettings.Value.IdentitySettings.StandardPassword
            });
        }
    }

    private async Task SeedRolesAsync()
    {
        await _identityService.AddRole(RoleConstants.Moderator);
        await _identityService.AddRole(RoleConstants.Standard);
    }
}