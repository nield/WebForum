namespace WebForum.Application.Features.Posts.Commands.CreateComment;

public class CreateCommentMapper : Profile
{
    public CreateCommentMapper()
    {
        CreateMap<CreateCommentCommand, Comment>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Post, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedDateTime, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());
    }
}
