using WebForum.Application.Common.Models;
using WebForum.Application.Features.Auth.Commands.RegisterUser;

namespace WebForum.Application.Tests.Features.Auth.Commands.RegisterUser;

public class RegisterUserCommandHandlerTests : BaseTestFixture
{
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_identityServiceMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_ShouldRegisterUser()
    {
        var command = Builder<RegisterUserCommand>.CreateNew().Build();

        await _handler.Handle(command, CancellationToken.None);

        _identityServiceMock.Verify(x => x.RegisterUser(It.IsAny<RegisterUserDto>()), Times.Once);
    }
}
