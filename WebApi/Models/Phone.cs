using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Phone: BaseModel
    {
        public int phoneId { get; set; }
        public string phoneNumber { get; set; }
        public int addressId { get; set; }

    }
}
