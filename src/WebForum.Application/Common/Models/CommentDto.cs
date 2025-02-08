namespace WebForum.Application.Common.Models;

public class CommentDto
{
    public DateTimeOffset CreatedDateTime { get; set; }
    public string Content { get; set; }
}
