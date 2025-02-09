using WebForum.Infrastructure.Persistence.Repositories;

namespace WebForum.Infrastructure.Tests.Persistence.Repositories;

public class PostRepositoryTests : BasePersistenceTestFixture<PostRepository>
{
    private readonly PostRepository _repository;

    public PostRepositoryTests()
    {
        _repository = new(_dbContext);
    }

    [Fact]
    public async Task Given_PostExists_Then_ReturnTrue()
    {
        var sut = await _repository.GetByIdAsync(1);

        sut.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenPostExist_When_Updating_Then_PostShouldBeUpdate()
    {
        var entity = await _repository.GetByIdAsync(1);

        entity.Should().NotBeNull();

        var newTitle = "updated title";

        entity.Title = newTitle;

        await _repository.UpdateAsync(entity);

        var updatedTemplate = await _repository.GetByIdAsync(1);

        updatedTemplate.Should().NotBeNull();
        updatedTemplate.Title.Should().Be(newTitle);
    }
}
