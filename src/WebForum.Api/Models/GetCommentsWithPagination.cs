using WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;

namespace WebForum.Api.Models;

public class GetCommentsWithPaginationRequest
{
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public string Author { get; set; }

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public class GetCommentsWithPaginationResponse : CommentDto
{
    
}

public class GetCommentsWithPaginationMapper : Profile
{
    public GetCommentsWithPaginationMapper()
    {
        CreateMap<GetCommentsWithPaginationRequest, GetCommentsWithPaginationQuery>();

        CreateMap<GetCommentsWithPaginationDto, GetCommentsWithPaginationResponse>()
            .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.Comment.CreatedDateTime))
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.Comment.AuthorName} {src.Comment.AuthorSurname}" ))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment.Content));
    }
}
