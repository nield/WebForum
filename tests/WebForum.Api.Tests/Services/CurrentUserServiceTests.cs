using System.Security.Claims;
using WebForum.Api.Services;
using Microsoft.AspNetCore.Http;
using WebForum.Application.Common.Constants;

namespace WebForum.Api.Tests.Services;

public class CurrentUserServiceTests
{
    private readonly CurrentUserService _currentUserService;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();

    public CurrentUserServiceTests()
    {
        _currentUserService = new(_httpContextAccessorMock.Object);
    }

    [Fact]
    public void Given_UserIdClaimExists_When_FetchingUserId_Then_ReturnsUserIdFromClaim()
    {
        var context = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, UserConstants.IntegrationTestUserId)
            ]))
        };

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserId.Should().Be(UserConstants.IntegrationTestUserId);
    }

    [Fact]
    public void Given_UserIdClaimDoesNotExists_When_FetchingUserId_Then_ReturnsNull()
    {
        var context = new DefaultHttpContext();

        _httpContextAccessorMock.SetupGet(x => x.HttpContext).Returns(context);

        _currentUserService.UserId.Should().BeNull();
    }   
}