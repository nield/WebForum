using WebForum.Application.Features.Posts.Commands.CreateComment;

namespace WebForum.Application.Tests.Features.Posts.Commands.CreateComment;

public class CreateCommentCommandHandlerTests : BaseTestFixture
{
    private readonly CreateCommentCommandHandler _handler;

    public CreateCommentCommandHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_postRepositoryMock.Object, _commentRepositoryMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenPostIdDoesNotExist_ShouldThrowException()
    {
        var command = Builder<CreateCommentCommand>.CreateNew().Build();

        _postRepositoryMock.Setup(x => x.ExistsAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(false);

       await Assert.ThrowsAsync<NotFoundException>(() => 
                        _handler.Handle(command, CancellationToken.None));

        _commentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Comment>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_GivenPostIdExists_ShouldSaveComment()
    {
        var command = Builder<CreateCommentCommand>.CreateNew().Build();

        _postRepositoryMock.Setup(x => x.ExistsAsync(command.PostId, CancellationToken.None))
            .ReturnsAsync(true);

        await _handler.Handle(command, CancellationToken.None);

        _commentRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Comment>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
