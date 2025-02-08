namespace WebForum.Api.Models;

public class CommentDto
{
    public DateTimeOffset CreatedDateTime { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
}


public class CommentDtoMapper : Profile
{
    public CommentDtoMapper()
    {
        CreateMap<Application.Common.Models.CommentDto, CommentDto>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.AuthorName} {src.AuthorSurname}"));
    }
}