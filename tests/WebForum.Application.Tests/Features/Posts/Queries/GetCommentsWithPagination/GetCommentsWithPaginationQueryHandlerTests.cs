using WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;

namespace WebForum.Application.Tests.Features.Posts.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationQueryHandlerTests : BaseTestFixture
{
    private readonly GetCommentsWithPaginationQueryHandler _handler;

    public GetCommentsWithPaginationQueryHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_applicationDbContextMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenNoComments_ShouldReturnEmptyItems()
    {
        var request = new GetCommentsWithPaginationQuery();

        var commentDbSetMock = new List<Comment>()
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(commentDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(0);
    }

    [Fact]
    public async Task Handle_GivenCommentsExists_ShouldReturnItems()
    {
        var postId = 1;

        var request = new GetCommentsWithPaginationQuery
        {
            PostId = postId
        };

        var comments = Builder<Comment>.CreateListOfSize(1)
            .All()
            .With(x => x.PostId, postId)
            .TheFirst(1)
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .Build();

        var commentDbSetMock = comments
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(commentDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(1);
    }

    [Fact]
    public async Task Handle_GivenFilterFromDate_ShouldReturnFilteredItems()
    {
        var postId = 1;

        var request = new GetCommentsWithPaginationQuery
        {
            FromDate = new DateOnly(2000, 1, 1),
            PostId = postId
        };

        var comments = Builder<Comment>.CreateListOfSize(4)
            .All()
            .With(x => x.PostId, postId)
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .TheFirst(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 10, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 23, 59, 59, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 2, 0, 0, 1, DateTimeKind.Utc))
            .Build();

        var commentDbSetMock = comments
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(commentDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(4);
    }

    [Fact]
    public async Task Handle_GivenFilterToDate_ShouldReturnFilteredItems()
    {
        var postId = 1;

        var request = new GetCommentsWithPaginationQuery
        {
            ToDate = new DateOnly(2000, 1, 2),
            PostId = postId
        };

        var comments = Builder<Comment>.CreateListOfSize(4)
            .All()
            .With(x => x.PostId, postId)
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .TheFirst(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 10, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 23, 59, 59, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 2, 0, 0, 1, DateTimeKind.Utc))
            .Build();

        var commentDbSetMock = comments
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(commentDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(4);
    }

    [Fact]
    public async Task Handle_GivenFilterBetweenDates_ShouldReturnFilteredItems()
    {
        var postId = 1;

        var request = new GetCommentsWithPaginationQuery
        {
            FromDate = new DateOnly(2000, 1, 1),
            ToDate = new DateOnly(2000, 1, 1),
            PostId = postId
        };

        var comments = Builder<Comment>.CreateListOfSize(4)
            .All()
            .With(x => x.PostId, postId)
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .TheFirst(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 10, 0, 0, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 1, 23, 59, 59, DateTimeKind.Utc))
            .TheNext(1)
            .With(x => x.CreatedDateTime, new DateTime(2000, 1, 2, 0, 0, 1, DateTimeKind.Utc))
            .Build();

        var postDbSetMock = comments
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(3);
    }

    [Theory]
    [InlineData("jane", 1)]
    [InlineData("john", 2)]
    [InlineData("test", 1)]
    [InlineData("kelly", 1)]
    public async Task Handle_GivenFilterAuthor_ShouldReturnFilteredItems(string author, int expectedItemCount)
    {
        var postId = 1;

        var request = new GetCommentsWithPaginationQuery
        {
            Author = author,
            PostId = postId,
        };

        var comments = Builder<Comment>.CreateListOfSize(4)
            .All()
            .With(x => x.PostId, postId)
            .TheFirst(1)
            .With(x => x.User, Builder<User>.CreateNew()
                                .With(x => x.Name, "john")
                                .With(x => x.Surname, "doe")
                                .Build())
            .TheNext(1)
            .With(x => x.User, Builder<User>.CreateNew()
                                .With(x => x.Name, "jane")
                                .With(x => x.Surname, "doe")
                                .Build())
            .TheNext(1)
            .With(x => x.User, Builder<User>.CreateNew()
                                .With(x => x.Name, "test")
                                .With(x => x.Surname, "user")
                                .Build())
            .TheNext(1)
            .With(x => x.User, Builder<User>.CreateNew()
                                .With(x => x.Name, "john")
                                .With(x => x.Surname, "kelly")
                                .Build())
            .Build();

        var commentDbSetMock = comments
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Comments)
            .Returns(commentDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(expectedItemCount);
    }
}
