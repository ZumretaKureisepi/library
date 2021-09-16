namespace Library.Model.DTO
{
    public class PublishersEditGetDto
    {
        public long PublisherId { get; set; }
        public long AdressId { get; set; }
        public string Name { get; set; }
        public string Road { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
