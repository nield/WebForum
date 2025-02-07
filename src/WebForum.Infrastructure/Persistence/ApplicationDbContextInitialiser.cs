using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebForum.Application.Common.Constants;
using WebForum.Application.Common.Settings;

namespace WebForum.Infrastructure.Persistence;

[ExcludeFromCodeCoverage]
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IOptions<AppSettings> _appSettings;

    public ApplicationDbContextInitialiser(
        ApplicationDbContext context,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<AppSettings> appSettings,
        ILogger<ApplicationDbContextInitialiser> logger
    )
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
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
            var result = await _userManager.CreateAsync(new User
            {
                Email = UserConstants.ModeratorUsername,
                UserName = UserConstants.ModeratorUsername,
                EmailConfirmed = true,
                TwoFactorEnabled = false
            }, _appSettings.Value.IdentitySettings.ModeratorPassword);

            if (result.Succeeded)
            {
                moderatorUser = await _userManager.FindByNameAsync(UserConstants.ModeratorUsername);

                if (!await _userManager.IsInRoleAsync(moderatorUser, RoleConstants.Moderator))
                {
                    await _userManager.AddToRoleAsync(moderatorUser, RoleConstants.Moderator);
                }
            }
        }
    }

    private async Task SeedRolesAsync()
    {
        var moderatorRole = await _roleManager.FindByNameAsync(RoleConstants.Moderator);

        if (moderatorRole is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(RoleConstants.Moderator));
        }
    }
}