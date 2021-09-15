using Library.Model.Filter;
using System.Linq;

namespace Library.Model.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            return query.Skip(filter.Skip())
                .Take(filter.PageSize);
        }
    }
}
