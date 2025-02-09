using WebForum.Application.Common.Constants;
using WebForum.Application.Features.Posts.Commands.TagPost;

namespace WebForum.Application.Tests.Features.Posts.Commands.TagPost;

public class TagPostCommandHandlerTests : BaseTestFixture
{
    private readonly TagPostCommandHandler _handler;

    public TagPostCommandHandlerTests(
        MappingFixture mappingFixture)
        : base(mappingFixture)
    {
        _handler = new(_postRepositoryMock.Object, _identityServiceMock.Object, _currentServiceMock.Object);
    }

    [Fact]
    public async Task Handle_GivenUserDoesNotHaveModeratorRole_ShouldThrowException()
    {
        var currentUser = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUser);

        _identityServiceMock.Setup(x => x.UserHasRole(currentUser, RoleConstants.Moderator))
            .ReturnsAsync(false);
        
        var command = Builder<TagPostCommand>.CreateNew().Build();

        await Assert.ThrowsAsync<ForbiddenAccessException>(() => 
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GivenPostDoesNotExists_ShouldThrowException()
    {
        var currentUser = UserConstants.ModeratorUsername;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUser);

        _identityServiceMock.Setup(x => x.UserHasRole(currentUser, RoleConstants.Moderator))
            .ReturnsAsync(true);

        var command = Builder<TagPostCommand>.CreateNew()
            .Build();

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(() => null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GivenModeratorUserAndPostExists_ShouldTag()
    {
        var currentUser = UserConstants.StandardUsername1;

        _currentServiceMock.SetupGet(x => x.UserId).Returns(currentUser);

        _identityServiceMock.Setup(x => x.UserHasRole(currentUser, RoleConstants.Moderator))
            .ReturnsAsync(true);

        List<string> tags =
        [
            "test1", "test2"
        ];

        var command = Builder<TagPostCommand>.CreateNew()
            .With(x => x.Tags, tags)
            .Build();

        var post = Builder<Post>.CreateNew()
            .With(x => x.Id, command.PostId)
            .With(x => x.Tags, [])
            .Build();

        Post savedPost = null;

        _postRepositoryMock.Setup(x => x.GetByIdAsync(command.PostId, It.IsAny<CancellationToken>()))           
            .ReturnsAsync(post);

        _postRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()))
             .Callback((Post p, CancellationToken _) => savedPost = p)
             .ReturnsAsync(post);

        await _handler.Handle(command, CancellationToken.None);

        savedPost.Should().NotBeNull();
        savedPost.Tags.Should().BeEquivalentTo(tags);
    }
}
