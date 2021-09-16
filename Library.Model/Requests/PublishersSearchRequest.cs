using Library.Model.Filter;

namespace Library.Model.Requests
{
    public class PublishersSearchRequest : PaginationFilter
    {
        public string Name { get; set; }
    }
}
