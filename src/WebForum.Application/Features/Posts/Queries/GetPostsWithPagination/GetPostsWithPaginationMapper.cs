namespace WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationMapper : Profile
{
    public GetPostsWithPaginationMapper()
    {
        CreateMap<Post, GetPostsWithPaginationDto>()
           .ForMember(dest => dest.LikedBy, opt => opt.MapFrom(src => src.Likes.Count))
           .ForMember(dest => dest.CreatedDateTime, opt => opt.MapFrom(src => src.CreatedDateTime))
           .ForMember(x => x.AuthorName, opt => opt.MapFrom(src => src.User.Name))
           .ForMember(x => x.AuthorSurname, opt => opt.MapFrom(src => src.User.Surname));
    }
}
