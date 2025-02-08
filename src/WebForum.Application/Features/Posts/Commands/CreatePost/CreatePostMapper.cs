namespace WebForum.Application.Features.Posts.Commands.CreatePost;

public class CreatePostMapper : Profile
{
    public CreatePostMapper()
    {
        CreateMap<CreatePostCommand, Post>()
            .ForMember(dest => dest.Comments, opt => opt.Ignore())
            .ForMember(dest => dest.Likes, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
    }
}
