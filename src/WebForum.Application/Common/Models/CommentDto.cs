namespace WebForum.Application.Common.Models;

public class CommentDto
{
    public DateTimeOffset CreatedDateTime { get; set; }
    public string Content { get; set; }
    public string AuthorName { get; set; }
    public string AuthorSurname { get; set; }
}
