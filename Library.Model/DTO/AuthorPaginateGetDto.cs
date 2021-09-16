using System.Collections.Generic;

namespace Library.Model.DTO
{
    public class AuthorPaginateGetDto
    {
        public List<AuthorGetDto> Authors { get; set; }
        public int AuthorsCount { get; set; }
    }
}
