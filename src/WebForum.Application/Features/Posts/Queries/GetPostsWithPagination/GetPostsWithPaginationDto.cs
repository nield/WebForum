namespace WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public string AuthorSurname { get; set; }
    public int LikedBy { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }

    public List<string> Tags { get; set; }
}
