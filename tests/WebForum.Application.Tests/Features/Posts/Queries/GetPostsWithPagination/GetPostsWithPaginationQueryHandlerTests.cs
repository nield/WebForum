using WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

namespace WebForum.Application.Tests.Features.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationQueryHandlerTests : BaseTestFixture
{
    private readonly GetPostsWithPaginationQueryHandler _handler;

    public GetPostsWithPaginationQueryHandlerTests(
        MappingFixture mappingFixture) 
        : base(mappingFixture)
    {
        _handler = new(_applicationDbContextMock.Object, _mapper);
    }

    [Fact]
    public async Task Handle_GivenNoPosts_ShouldReturnEmptyItems()
    {
        var request = new GetPostsWithPaginationQuery();

        var postDbSetMock = new List<Post>()
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(0);
    }

    [Fact]
    public async Task Handle_GivenPostsExists_ShouldReturnItems()
    {
        var request = new GetPostsWithPaginationQuery();

        var posts = Builder<Post>.CreateListOfSize(1)
            .TheFirst(1)
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
        sut.Items.Count.Should().Be(1);
    }

    [Fact]
    public async Task Handle_GivenFilterFromDate_ShouldReturnFilteredItems()
    {
        var request = new GetPostsWithPaginationQuery
        {
            FromDate = new DateOnly(2000, 1, 1)
        };

        var posts = Builder<Post>.CreateListOfSize(4)
            .All()            
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
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

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(4);
    }

    [Fact]
    public async Task Handle_GivenFilterToDate_ShouldReturnFilteredItems()
    {
        var request = new GetPostsWithPaginationQuery
        {
            ToDate = new DateOnly(2000, 1, 2)
        };

        var posts = Builder<Post>.CreateListOfSize(4)
            .All()
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
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

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(4);
    }

    [Fact]
    public async Task Handle_GivenFilterBetweenDates_ShouldReturnFilteredItems()
    {
        var request = new GetPostsWithPaginationQuery
        {
            FromDate = new DateOnly(2000, 1, 1),
            ToDate = new DateOnly(2000, 1, 1)
        };

        var posts = Builder<Post>.CreateListOfSize(4)
            .All()
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
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

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(3);
    }

    [Fact]
    public async Task Handle_GivenFilterTags_ShouldReturnFilteredItems()
    {
        var request = new GetPostsWithPaginationQuery
        {
            Tags = ["test"]
        };

        var posts = Builder<Post>.CreateListOfSize(4)
            .All()
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
            .With(x => x.User, Builder<User>.CreateNew().Build())
            .TheFirst(1)
            .With(x => x.Tags, [])
            .TheNext(1)
            .With(x => x.Tags, ["one"])
            .TheNext(1)
            .With(x => x.Tags, ["two", "test"])
            .TheNext(1)
            .With(x => x.Tags, ["test"])
            .Build();

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(2);
    }

    [Theory]
    [InlineData("jane", 1)]
    [InlineData("john", 2)]
    [InlineData("test", 1)]
    [InlineData("kelly", 1)]
    public async Task Handle_GivenFilterAuthor_ShouldReturnFilteredItems(string author, int expectedItemCount)
    {
        var request = new GetPostsWithPaginationQuery
        {
            Author = author
        };

        var posts = Builder<Post>.CreateListOfSize(4)
            .All()
            .With(x => x.Comments, [])
            .With(x => x.Likes, [])
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

        var postDbSetMock = posts
            .AsQueryable()
            .BuildMockDbSet();

        _applicationDbContextMock.SetupGet(x => x.Posts)
            .Returns(postDbSetMock.Object);

        var sut = await _handler.Handle(request, CancellationToken.None);

        sut.Should().NotBeNull();
        sut.Items.Count.Should().Be(expectedItemCount);
    }
}
