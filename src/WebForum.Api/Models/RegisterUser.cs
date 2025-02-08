using FluentValidation;
using WebForum.Application.Common.Constants;
using WebForum.Application.Features.Auth.Commands.RegisterUser;

namespace WebForum.Api.Models;

public class RegisterUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class RegisterUserRequestMapper : Profile
{
    public RegisterUserRequestMapper()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => RoleConstants.Standard));
    }
}

public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Surname).NotEmpty();
    }
}
