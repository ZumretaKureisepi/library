using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.DTO
{
    public class PublisherPaginateGetDto
    {
        public List<PublisherGetDto> Publishers { get; set; }
        public int PublishersCount { get; set; }
    }
}
