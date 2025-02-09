using WebForum.Application.Common.Constants;
using WebForum.Application.Features.Posts.Commands.AddLike;

namespace WebForum.Application.Tests.Features.Posts.Commands.AddLike;

public class AddLikeCommandHandlerTests : BaseTestFixture
{
    private readonly AddLikeCommandHandler _handler;

    public AddLikeCommandHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_postRepositoryMock.Object,
            _likeRepositoryMock.Object,
            _currentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GivenPostIdDoesNotExists_ShouldThrowException()
    {
        var command = Builder<AddLikeCommand>.CreateNew().Build();

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(() => null);

        await Assert.ThrowsAsync<NotFoundException>(() => 
            _handler.Handle(command, CancellationToken.None));

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenPostUserTriesToLikeOwnPost_ShouldThrowException()
    {
        var command = Builder<AddLikeCommand>.CreateNew().Build();

        _currentServiceMock.SetupGet(x => x.UserId).Returns(UserConstants.StandardUsername1);

        var post = Builder<Post>.CreateNew()
            .With(x => x.CreatedBy, UserConstants.StandardUsername1)
            .Build();

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(post);

        var sut = await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(command, CancellationToken.None));

        sut.Message.Should().StartWith("Users not allowed to like their own posts");

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenUserTriesToLikeAPostTwice_ShouldThrowException()
    {
        var command = Builder<AddLikeCommand>.CreateNew().Build();

        var currentUser = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUser);

        var post = Builder<Post>.CreateNew()
            .With(x => x.CreatedBy, UserConstants.StandardUsername2)
            .Build();

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(post);

        var like = Builder<Like>.CreateNew()
            .With(x => x.PostId, post.Id)
            .With(x => x.CreatedBy, currentUser)
            .Build();

        _likeRepositoryMock.Setup(x => x.GetLikeAsync(post.Id, currentUser, CancellationToken.None))
            .ReturnsAsync(like);

        var sut = await Assert.ThrowsAsync<BadRequestException>(() =>
            _handler.Handle(command, CancellationToken.None));

        sut.Message.Should().StartWith("User already liked the post");

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenUserTriesToLikeADifferentUsersPostOnce_ShouldSaveLike()
    {
        var command = Builder<AddLikeCommand>.CreateNew().Build();

        var currentUser = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUser);

        var post = Builder<Post>.CreateNew()
            .With(x => x.CreatedBy, UserConstants.StandardUsername2)
            .Build();

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(post);

        _likeRepositoryMock.Setup(x => x.GetLikeAsync(post.Id, currentUser, CancellationToken.None))
            .ReturnsAsync(() => null);

        await _handler.Handle(command, CancellationToken.None);

        _likeRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Like>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
