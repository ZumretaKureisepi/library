using System.Collections.Generic;

namespace Library.Model.DTO
{
    public class BookPaginateGetDto
    {
        public List<BookGetDto> Books { get; set; }
        public int BooksCount { get; set; }
    }
}
