using System.Diagnostics.CodeAnalysis;
using WebForum.Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace System.Linq;

[ExcludeFromCodeCoverage]
public static class QueryableExtensions
{
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        where T : class
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}