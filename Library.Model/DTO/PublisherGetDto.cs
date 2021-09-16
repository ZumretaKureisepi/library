namespace Library.Model.DTO
{
    public class PublisherGetDto
    {
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public AdressGetDto Adress { get; set; }
        public long AdressId { get; set; }
    }
}
