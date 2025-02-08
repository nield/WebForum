namespace WebForum.Api.Models;

public class CommentDto
{
    public DateTimeOffset CreatedDateTime { get; set; }
    public string Content { get; set; }
}


public class CommentDtoMapper : Profile
{
    public CommentDtoMapper()
    {
        CreateMap<Application.Common.Models.CommentDto, CommentDto>();
    }
}