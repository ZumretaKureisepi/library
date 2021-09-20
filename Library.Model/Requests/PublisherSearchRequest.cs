using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class PublisherSearchRequest : PaginationFilter
    {
        public string Name { get; set; }
    }
}
