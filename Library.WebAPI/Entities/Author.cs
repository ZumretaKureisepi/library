using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Models
{
    public class Author
    {
        public long AuthorId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<AuthBook> AuthBooks { get; set; }

    }
}
    