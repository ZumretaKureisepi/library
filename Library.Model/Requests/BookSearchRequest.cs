using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class BookSearchRequest : PaginationFilter
    {
        public string Title { get; set; }

    }
}
