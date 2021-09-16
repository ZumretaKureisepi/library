using System.Collections.Generic;

namespace Library.Model.DTO
{
    public class BookGetDto
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<long> AuthorIds { get; set; }
        public long? PublisherId { get; set; }
    }
}
