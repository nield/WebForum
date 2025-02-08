namespace WebForum.Application.Features.Posts.Commands.TagPost;

public class TagPostCommand : IRequest
{
    public long PostId { get; set; }
    public List<string> Tags { get; set; }
}

public class TagPostCommandHandler : IRequestHandler<TagPostCommand>
{
    private readonly IPostRepository _postRepository;

    public TagPostCommandHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task Handle(TagPostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        post.Tags = request.Tags;

        await _postRepository.UpdateAsync(post, cancellationToken);
    }
}
