﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.WebAPI.Models
{
    public class AuthBook
    {
        public long AuthBookId { get; set; }

        public long AuthorId { get; set; }
        public Author Author { get; set; }

        public long? BookId { get; set; }
        public Book Book { get; set; }

        public bool IsDeleted { get; set; }



    }
}