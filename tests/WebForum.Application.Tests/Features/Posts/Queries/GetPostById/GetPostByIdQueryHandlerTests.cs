using WebForum.Application.Features.Posts.Queries.GetPostById;

namespace WebForum.Application.Tests.Features.Posts.Queries.GetPostById;

public class GetPostByIdQueryHandlerTests : BaseTestFixture
{
    private readonly GetPostByIdQueryHandler _handler;

    public GetPostByIdQueryHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_applicationDbContextMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenPostDoesNotExist_ShouldThrowException()
    {
        var postDbSetMock = new List<Post>().AsQueryable().BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var request = Builder<GetPostByIdQuery>.CreateNew().Build();

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GivenPostExist_ShouldReturnData()
    {
        var request = Builder<GetPostByIdQuery>.CreateNew().Build();

        var posts = Builder<Post>.CreateListOfSize(1)
            .TheFirst(1)
            .With(x => x.Id, request.Id)
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .Build();

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
    }
}
