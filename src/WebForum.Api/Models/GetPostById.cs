using WebForum.Application.Features.Posts.Queries.GetPostById;

namespace WebForum.Api.Models;

public class GetPostByIdResponse
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int LikedBy { get; set; }

    public List<string> Tags { get; set; }
    public List<CommentDto> Comments { get; set; }
}

public class GetPostByIdMapper : Profile
{
    public GetPostByIdMapper()
    {
        CreateMap<GetPostByIdDto, GetPostByIdResponse>();
    }
}
