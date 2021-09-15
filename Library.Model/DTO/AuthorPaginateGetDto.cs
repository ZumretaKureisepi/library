using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.DTO
{
    public class AuthorPaginateGetDto
    {
        public List<AuthorGetDto> Authors { get; set; }
        public int AuthorsCount { get; set; }
    }
}
