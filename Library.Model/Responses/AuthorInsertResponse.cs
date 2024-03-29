﻿using System;
using System.Collections.Generic;

namespace Library.Model.Responses
{
    public class AuthorInsertResponse
    {
        public string Name { get; set; }
        public string Biography { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public List<long> BookIds { get; set; }
    }
}
