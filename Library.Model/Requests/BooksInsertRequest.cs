using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Requests
{
    public class BooksInsertRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public long BookId { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public long? PublisherId { get; set; }
        public string Image { get; set; }
        public List<long> AuthorIds { get; set; }



    }
}
