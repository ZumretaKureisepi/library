using Library.Model.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Requests
{
    public class BooksSearchRequest:PaginationFilter
    {
        public string Title { get; set; }

    }
}
