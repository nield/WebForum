using WebForum.Application.Common.Constants;

namespace WebForum.Application.Features.Posts.Commands.TagPost;

public class TagPostCommand : IRequest
{
    public long PostId { get; set; }
    public List<string> Tags { get; set; }
}

public class TagPostCommandHandler : IRequestHandler<TagPostCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly IIdentityService _identityService;
    private readonly ICurrentUserService _currentUserService;

    public TagPostCommandHandler(
        IPostRepository postRepository,
        IIdentityService identityService,
        ICurrentUserService currentUserService)
    {
        _postRepository = postRepository;
        _identityService = identityService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(TagPostCommand request, CancellationToken cancellationToken)
    {
        //Role is checked on controller, just some extra security
        if (!await _identityService.UserHasRole(_currentUserService.UserId, RoleConstants.Moderator))
        {
            throw new ForbiddenAccessException();
        }

        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken);

        if (post is null)
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        post.Tags = request.Tags;

        await _postRepository.UpdateAsync(post, cancellationToken);
    }
}
