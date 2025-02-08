using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace WebForum.Api.Integration.Tests.Controllers;

[Collection("WebApplicationCollection")]
public class PostsControllerTests
{
    private readonly WebApplicationFixture _webApplicationFixture;

    public PostsControllerTests(WebApplicationFixture webApplicationFixture)
    {
        _webApplicationFixture = webApplicationFixture;
    }

    [Fact]
    public async Task CreatePost_ShouldReturn201()
    {
        try
        {
            var payload = new CreatePostRequest
            {
                Title = "Testing Create Item",
                Content = "Test Content"
            };

            var sut = await _webApplicationFixture.HttpClient.PostAsync(
                "/api/posts",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            sut.StatusCode.Should().Be(HttpStatusCode.Created);
        }
        finally
        {
            await _webApplicationFixture.ResetDatabaseAsync();
        }
    }

    [Fact]
    public async Task GetPostById_GivenIdExist_ShouldReturn200()
    {
        var response = await _webApplicationFixture.HttpClient.GetAsync("/api/posts/1");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<GetPostByIdResponse>();

        data.Should().NotBeNull();
        data!.Id.Should().Be(1);
        data.Comments.Count.Should().Be(2);
        data.LikedBy.Should().Be(1);
    }

    [Fact]
    public async Task GetPostsWithPagination_GivenPostsExist_ShouldReturn200()
    {
        var response = await _webApplicationFixture.HttpClient.GetAsync("/api/posts");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<PaginatedListResponse<GetPostsWithPaginationResponse>>();

        data.Should().NotBeNull();
        data.Items.Count.Should().Be(1);
        data.PageNumber.Should().Be(1);
        data.PageSize.Should().Be(10);
        data.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task CreateComment_GivenPostExists_ShouldReturn201()
    {
        try
        {
            var payload = new CreateCommentRequest
            {
                Comment = "Testing comment add"
            };

            var sut = await _webApplicationFixture.HttpClient.PostAsync(
                "/api/posts/1/comments",
                new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json")
            );

            sut.StatusCode.Should().Be(HttpStatusCode.Created);

        }
        finally
        {
            await _webApplicationFixture.ResetDatabaseAsync();
        }        
    }

    [Fact]
    public async Task GetCommentsWithPagination_GivenPostsAndCommentsExist_ShouldReturn200()
    {
        var response = await _webApplicationFixture.HttpClient.GetAsync("/api/posts/1/comments");

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var data = await response.Content.ReadFromJsonAsync<PaginatedListResponse<GetCommentsWithPaginationResponse>>();

        data.Should().NotBeNull();
        data.Items.Count.Should().Be(2);
        data.PageNumber.Should().Be(1);
        data.PageSize.Should().Be(10);
        data.TotalPages.Should().Be(1);
    }

    [Fact]
    public async Task AddLike_GivenPostAndLikeTheSameUser_ShouldReturn400()
    {
        try
        {
            var sut = await _webApplicationFixture.HttpClient.PostAsync(
                "/api/posts/1/likes/add", null);

            sut.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        finally
        {
            await _webApplicationFixture.ResetDatabaseAsync();
        }
    }
}
