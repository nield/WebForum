using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Posts.Queries.GetPostById;

public class GetPostByIdMapper : Profile
{
    public GetPostByIdMapper()
    {
        CreateMap<Comment, CommentDto>()
            .ForMember(x => x.CreatedDateTime, opt => opt.MapFrom(src => src.CreatedDateTime))
            .ForMember(x => x.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(x => x.AuthorName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(x => x.AuthorSurname, opt => opt.MapFrom(src => src.User.Surname));

        CreateMap<Post, GetPostByIdDto>()
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ForMember(dest => dest.LikedBy, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(x => x.AuthorName, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(x => x.AuthorSurname, opt => opt.MapFrom(src => src.User.Surname));
    }
}
