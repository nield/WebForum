using WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

namespace WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationMapper : Profile
{
    public GetCommentsWithPaginationMapper()
    {
        CreateMap<Comment, GetCommentsWithPaginationDto>()
           .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src));
    }
}
