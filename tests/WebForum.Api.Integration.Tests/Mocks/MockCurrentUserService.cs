using WebForum.Application.Common.Constants;
using WebForum.Application.Common.Interfaces;

namespace WebForum.Api.Integration.Tests.Mocks;

public class MockCurrentUserService : ICurrentUserService
{
    public string? UserId => UserConstants.IntegrationTestUserId;
}
