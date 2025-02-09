using WebForum.Application.Common.Constants;
using WebForum.Application.Features.Posts.Commands.RemoveLike;

namespace WebForum.Application.Tests.Features.Posts.Commands.RemoveLike;

public class RemoveLikeCommandHandlerTests : BaseTestFixture
{
    private readonly RemoveLikeCommandHandler _handler;

    public RemoveLikeCommandHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_postRepositoryMock.Object, _likeRepositoryMock.Object, _currentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GivenPostIdDoesNotExists_ShouldThrowException()
    {
        var command = Builder<RemoveLikeCommand>.CreateNew().Build();

        _postRepositoryMock.Setup(x => x.ExistsAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenUserHasNotLikedPost_ShouldThrowException()
    {
        var command = Builder<RemoveLikeCommand>.CreateNew().Build();

        var currentUserId = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUserId);

        _likeRepositoryMock.Setup(x => x.GetLikeAsync(command.PostId, currentUserId, CancellationToken.None))
            .ReturnsAsync(() => null);

        _postRepositoryMock.Setup(x => x.ExistsAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(true);

        var sut = await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(command, CancellationToken.None));

        sut.Message.Should().StartWith("User has not liked the post");

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenUserHasLikedAPost_ShouldRemoveLike()
    {
        var command = Builder<RemoveLikeCommand>.CreateNew().Build();

        var currentUserId = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUserId);

        var like = Builder<Like>.CreateNew()
            .With(x => x.PostId, command.PostId)
            .With(x => x.CreatedBy, currentUserId)
            .Build();   

        _likeRepositoryMock.Setup(x => x.GetLikeAsync(command.PostId, currentUserId, CancellationToken.None))
            .ReturnsAsync(like);

        _postRepositoryMock.Setup(x => x.ExistsAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(true);

        await _handler.Handle(command, CancellationToken.None);

        _likeRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
