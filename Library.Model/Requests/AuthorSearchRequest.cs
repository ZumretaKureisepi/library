using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class AuthorSearchRequest : PaginationFilter
    {
        public string Name { get; set; }
        public long BookId { get; set; }
    }
}
