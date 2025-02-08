using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using WebForum.Application.Common.Exceptions;
using WebForum.Application.Common.Models;

namespace WebForum.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task AddRole(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(x => new ValidationFailure(x.Code, x.Description));

                throw new ValidationException(errorMessages);
            }
        }
    }

    public async Task RegisterUser(RegisterUserDto register)
    {
        if (!await _roleManager.RoleExistsAsync(register.RoleName))
        {
            throw new BadRequestException($"Invalid role. Role: {register.RoleName}");
        }

        var newUser = new User
        {
            Email = register.Email,
            UserName = register.Email,
            Name = register.Name,
            Surname = register.Surname,
        };

        var result = await _userManager.CreateAsync(newUser, register.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(newUser, register.RoleName);
        }
        else
        {
            var errorMessages = result.Errors.Select(x => new ValidationFailure(x.Code, x.Description));

            throw new ValidationException(errorMessages);
        }
    }
}
