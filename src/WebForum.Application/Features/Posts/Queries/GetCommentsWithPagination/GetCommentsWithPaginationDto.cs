using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationDto
{
    public CommentDto Comment { get; set; }
}
