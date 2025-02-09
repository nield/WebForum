using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommand : IRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string RoleName { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IIdentityService identityService, IMapper mapper)
    {
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<RegisterUserDto>(request);

        await _identityService.RegisterUser(newUser);
    }
}
