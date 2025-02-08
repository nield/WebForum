namespace WebForum.Application.Features.Posts.Commands.RemoveLike;

public class RemoveLikeCommand : IRequest
{
    public long PostId { get; set; }
}

public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly ICurrentUserService _currentUserService;

    public RemoveLikeCommandHandler(
        IPostRepository postRepository,
        ILikeRepository likeRepository,
        ICurrentUserService currentUserService)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(
        RemoveLikeCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _postRepository.ExistsAsync(request.PostId, cancellationToken))
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        var userLikedPost = await _likeRepository.GetLikeAsync(request.PostId, _currentUserService.UserId, cancellationToken);

        if (userLikedPost is null)
        {
            throw new BadRequestException($"User has not liked the post. PostId: {request.PostId}");
        }

        await _likeRepository.DeleteAsync(userLikedPost, cancellationToken);
    }
}