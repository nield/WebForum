using WebForum.Application.Common.Constants;
using WebForum.Infrastructure.Persistence.Repositories;

namespace WebForum.Infrastructure.Tests.Persistence.Repositories;

public class LikeRepositoryTests : BasePersistenceTestFixture<LikeRepository>
{
    private readonly LikeRepository _repository;

    public LikeRepositoryTests()
    {
        _repository = new(_dbContext);
    }

    [Fact]
    public async Task GetLikeAsync_GivenLikeExists_ShouldReturnData()
    {
        var sut = await _repository.GetLikeAsync(1, UserConstants.IntegrationTestUserId, CancellationToken.None);

        sut.Should().NotBeNull();
    }
}
