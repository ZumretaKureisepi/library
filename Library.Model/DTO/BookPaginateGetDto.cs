using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.DTO
{
    public class BookPaginateGetDto
    {
        public List<BookGetDto> Books { get; set; }
        public int BooksCount { get; set; }
    }
}
