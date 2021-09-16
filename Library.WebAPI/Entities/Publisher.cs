using System.ComponentModel.DataAnnotations.Schema;

namespace Library.WebAPI.Models
{
    public class Publisher
    {
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("AdressId")]
        public Adress Adress { get; set; }
        public long AdressId { get; set; }
    }
}
