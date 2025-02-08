using Microsoft.EntityFrameworkCore;
using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationQuery : IRequest<PaginatedList<GetPostsWithPaginationDto>>
{
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public string Author { get; set; }
    public List<string> Tags { get; set; }

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public class GetPostsWithPaginationQueryHandler : IRequestHandler<GetPostsWithPaginationQuery, PaginatedList<GetPostsWithPaginationDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetPostsWithPaginationQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetPostsWithPaginationDto>> Handle(
        GetPostsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var querablePosts = _applicationDbContext.Posts.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Author))
        {
            querablePosts = querablePosts.Include(x => x.User)
                                .Where(x => x.User.Name.Contains(request.Author)
                                        || x.User.Surname.Contains(request.Author));
        }

        if (request.Tags.Count != 0)
        {
            querablePosts = querablePosts.Where(x =>
                request.Tags.Any(requestTag => x.Tags.Contains(requestTag)));
        }

        if (request.FromDate.HasValue)
        {
            var fromDate = new DateTime(request.FromDate.Value, TimeOnly.MinValue, DateTimeKind.Utc);
            querablePosts = querablePosts.Where(x => x.CreatedDateTime > fromDate);
        }

        if (request.ToDate.HasValue)
        {
            var toDate = new DateTime(request.ToDate.Value, TimeOnly.MaxValue, DateTimeKind.Utc);
            querablePosts = querablePosts.Where(x => x.CreatedDateTime < toDate);
        }

        var pagedData = await querablePosts.OrderBy(x => x.Title)
            .ProjectTo<GetPostsWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return pagedData;
    }
}