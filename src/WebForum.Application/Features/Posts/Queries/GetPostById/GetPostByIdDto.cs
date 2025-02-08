using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Posts.Queries.GetPostById;

public  class GetPostByIdDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public string AuthorSurname { get; set; }
    public int LikedBy { get; set; }

    public List<string> Tags { get; set; }
    public List<CommentDto> Comments { get; set; }
}
