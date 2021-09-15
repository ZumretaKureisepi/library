using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Responses
{
    public class BooksInsertResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public long? PublisherId { get; set; }
        public List<long> AuthorIds { get; set; }

    }
}
