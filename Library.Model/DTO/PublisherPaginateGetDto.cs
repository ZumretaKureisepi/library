using System.Collections.Generic;

namespace Library.Model.DTO
{
    public class PublisherPaginateGetDto
    {
        public List<PublisherGetDto> Publishers { get; set; }
        public int PublishersCount { get; set; }
    }
}
