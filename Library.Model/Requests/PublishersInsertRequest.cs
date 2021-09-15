using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Model.Requests
{
    public class PublishersInsertRequest
    {
        public string Name { get; set; }
        public string Road { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public long PublisherId { get; set; }
        public long AdressId { get; set; }





    }
}
