using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class AuthorsSearchRequest : PaginationFilter
    {
        public string Name { get; set; }
        public long BookId { get; set; }
    }
}
