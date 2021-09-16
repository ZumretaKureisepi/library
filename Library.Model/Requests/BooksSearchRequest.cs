using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class BooksSearchRequest : PaginationFilter
    {
        public string Title { get; set; }

    }
}
