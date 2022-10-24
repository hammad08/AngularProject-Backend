using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Address: BaseModel
    {
        public int addressId { get; set; }

        public string addressType { get; set; }

        public string  street1 { get; set; }
        public string street2 { get; set; }

        public string city { get; set; }

        public string state { get; set; }

        public string zipcode { get; set; }

        public int customerId { get; set; }

        public List<Phone> phones { get; set; }

    }
}
