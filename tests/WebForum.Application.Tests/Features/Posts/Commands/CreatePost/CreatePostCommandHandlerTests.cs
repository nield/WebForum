using WebForum.Application.Features.Posts.Commands.CreatePost;

namespace WebForum.Application.Tests.Features.Posts.Commands.CreatePost;

public class CreatePostCommandHandlerTests : BaseTestFixture
{
    private readonly CreatePostCommandHandler _handler;

    public CreatePostCommandHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_postRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldSavePost()
    {
        var request = Builder<CreatePostCommand>.CreateNew().Build();

        await _handler.Handle(request, CancellationToken.None);

        _postRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Post>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
