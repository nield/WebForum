namespace WebForum.Application.Features.Posts.Commands.CreateComment;

public class CreateCommentCommand : IRequest<long>
{
    public long PostId { get; set; }
    public string Comment { get; set; }
}

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, long>
{
    private readonly IPostRepository _postRepository;
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CreateCommentCommandHandler(
        IPostRepository postRepository,
        ICommentRepository commentRepository,
        IMapper mapper)
    {
        _postRepository = postRepository;
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task<long> Handle(
        CreateCommentCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _postRepository.ExistsAsync(request.PostId, cancellationToken))
        {
            throw new NotFoundException(nameof(Post), request.PostId);
        }

        var newComment = _mapper.Map<Comment>(request);

        await _commentRepository.AddAsync(newComment, cancellationToken);

        return newComment.Id;
    }
}
