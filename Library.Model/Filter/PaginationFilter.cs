namespace Library.Model.Filter
{
    public class PaginationFilter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int Skip()
        {
            return (CurrentPage - 1) * PageSize;
        }

    }
}
