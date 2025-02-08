using WebForum.Application.Common.Models;

namespace WebForum.Api.Models;

public class PaginatedListResponse<T>
{
    public List<T> Items { get; set; } = null!;
    public int PageNumber { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
}

public class PagedListProfile : Profile
{
    public PagedListProfile()
    {
        CreateMap(typeof(PaginatedList<>), typeof(PaginatedListResponse<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}

public class PagedListConverter<TSource, TDest>
    : ITypeConverter<PaginatedList<TSource>, PaginatedListResponse<TDest>>
{
    public PaginatedListResponse<TDest> Convert(PaginatedList<TSource> source, PaginatedListResponse<TDest> destination, ResolutionContext context)
    {
        var mappedItems = context.Mapper.Map<List<TDest>>(source.Items);

        return new PaginatedListResponse<TDest>
        {
            Items = mappedItems,
            PageSize = source.PageSize,
            PageNumber = source.PageNumber,
            TotalCount = source.TotalCount,
            TotalPages = source.TotalPages,
            HasNextPage = source.HasNextPage,
            HasPreviousPage = source.HasPreviousPage
        };
    }
}