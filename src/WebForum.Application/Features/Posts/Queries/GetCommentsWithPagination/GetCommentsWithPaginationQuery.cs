using Microsoft.EntityFrameworkCore;
using WebForum.Application.Common.Models;

namespace WebForum.Application.Features.Posts.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationQuery : IRequest<PaginatedList<GetCommentsWithPaginationDto>>
{
    public long PostId { get; set; }
    public DateOnly? FromDate { get; set; }
    public DateOnly? ToDate { get; set; }
    public string Author { get; set; }

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public class GetCommentsWithPaginationQueryHandler : IRequestHandler<GetCommentsWithPaginationQuery, PaginatedList<GetCommentsWithPaginationDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public GetCommentsWithPaginationQueryHandler(
        IApplicationDbContext applicationDbContext,
        IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetCommentsWithPaginationDto>> Handle(
        GetCommentsWithPaginationQuery request, 
        CancellationToken cancellationToken)
    {
        var querableComments = _applicationDbContext.Comments.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Author))
        {
            querableComments = querableComments.Include(x => x.User)
                                .Where(x => x.User.Name.Contains(request.Author)
                                        || x.User.Surname.Contains(request.Author));
        }

        querableComments = querableComments.Where(x => x.PostId == request.PostId);
        
        if (request.FromDate.HasValue)
        {
            var fromDate = new DateTime(request.FromDate.Value, TimeOnly.MinValue, DateTimeKind.Utc);
            querableComments = querableComments.Where(x => x.CreatedDateTime >= fromDate);
        }

        if (request.ToDate.HasValue)
        {
            var toDate = new DateTime(request.ToDate.Value, TimeOnly.MaxValue, DateTimeKind.Utc);
            querableComments = querableComments.Where(x => x.CreatedDateTime <= toDate);
        }

        var pagedData = await querableComments
            .ProjectTo<GetCommentsWithPaginationDto>(_mapper.ConfigurationProvider)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);

        return pagedData;
    }
}
