namespace WebForum.Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommand : IRequest<long>
{
    public string Title { get; set; }
    public string Content { get; set; }
}

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, long>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public CreatePostCommandHandler(
        IPostRepository postRepository,
        IMapper mapper)
    {
        _postRepository = postRepository;
        _mapper = mapper;
    }

    public async Task<long> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var newPost = _mapper.Map<Post>(request);

        await _postRepository.AddAsync(newPost, cancellationToken);

        return newPost.Id;
    }
}