using System.Collections.Generic;

namespace Library.WebAPI.Models
{
    public class Book
    {
        public long BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Pages { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }

        public long? PublisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<AuthBook> AuthBooks { get; set; }

    }
}
