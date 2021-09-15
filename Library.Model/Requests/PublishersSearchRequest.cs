using Library.Model.Filter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Requests
{
    public class PublishersSearchRequest: PaginationFilter
    {
        public string Name { get; set; }

    }
}
