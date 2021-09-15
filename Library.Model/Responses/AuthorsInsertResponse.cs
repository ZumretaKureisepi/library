using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Responses
{
    public class AuthorsInsertResponse
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public List<long> BookIds { get; set; }

    }
}
