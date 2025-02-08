namespace WebForum.Application.Features.Posts.Commands.AddLike;

public class AddLikeCommand : IRequest
{
    public long PostId { get; set; }
}

public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand>
{
    private readonly IPostRepository _postRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddLikeCommandHandler(
        IPostRepository postRepository,
        ILikeRepository likeRepository,
        ICurrentUserService currentUserService)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _currentUserService = currentUserService;
    }

    public async Task Handle(AddLikeCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.PostId, cancellationToken)
            ?? throw new NotFoundException(nameof(Post), request.PostId);

        if (post.CreatedBy.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
        {
            throw new BadRequestException($"Users not allowed to like their own posts. PostId: {request.PostId}");
        }

        var userLikedPost = await _likeRepository.GetLikeAsync(request.PostId, _currentUserService.UserId, cancellationToken);

        if (userLikedPost is not null)
        {
            throw new BadRequestException($"User already liked the post. PostId: {request.PostId}");
        }

        await _likeRepository.AddAsync(new Like { PostId = request.PostId }, cancellationToken);
    }
}
