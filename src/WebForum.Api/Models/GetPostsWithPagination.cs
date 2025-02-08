using WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

namespace WebForum.Api.Models;

public class GetPostsWithPaginationRequest
{
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public string Author { get; set; }
    public List<string> Tags { get; set; } = [];

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public class GetPostsWithPaginationResponse
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int LikedBy { get; set; }
    public string Author { get; set; }
    public DateTimeOffset CreatedDateTime { get; set; }

    public List<string> Tags { get; set; }
}

public class GetPostsWithPaginationMapper : Profile
{
    public GetPostsWithPaginationMapper()
    {
        CreateMap<GetPostsWithPaginationRequest, GetPostsWithPaginationQuery>();

        CreateMap<GetPostsWithPaginationDto, GetPostsWithPaginationResponse>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => $"{src.AuthorName} {src.AuthorSurname}"));
    }
}
