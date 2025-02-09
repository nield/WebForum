using WebForum.Application.Common.Models;

namespace WebForum.Application.Common.Interfaces;

public interface IIdentityService
{
    Task RegisterUser(RegisterUserDto register);
    Task AddRole(string roleName);
    Task<bool> UserHasRole(string userId, string roleName);
}
