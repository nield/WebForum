using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
    {
        CreateMap<RegisterUserCommand, RegisterUserDto>();
    }
}
