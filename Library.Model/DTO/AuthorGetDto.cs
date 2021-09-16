using System;
using System.Collections.Generic;

namespace Library.Model.DTO
{
    public class AuthorGetDto
    {
        public long AuthorId { get; set; }
        public AuthorGetDto Author { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string BirthdayStr => DateOfBirth != null ? DateOfBirth.ToString("dd'/'MM'/'yyyy") : "";
        public string Email { get; set; }
        public string Image { get; set; }
        public List<long?> BookIds { get; set; }
    }
}
