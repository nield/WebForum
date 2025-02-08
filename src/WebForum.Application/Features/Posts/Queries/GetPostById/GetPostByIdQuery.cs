using Microsoft.EntityFrameworkCore;

namespace WebForum.Application.Features.Posts.Queries.GetPostById;

public class GetPostByIdQuery : IRequest<GetPostByIdDto>
{
    public long Id { get; set; }
}

public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQuery, GetPostByIdDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetPostByIdQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<GetPostByIdDto> Handle(
        GetPostByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var post = await _applicationDbContext.Posts
                            .AsNoTracking()
                            .Include(x => x.Comments)
                            .Include(x => x.Likes)
                            .Where(x => x.Id == request.Id)
                            .OrderBy(x => x.Title)
                            .ProjectTo<GetPostByIdDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

       return post ?? throw new NotFoundException(nameof(Post), request.Id);        
    }
}