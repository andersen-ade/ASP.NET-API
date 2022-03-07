using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAppWebAPI.Models.DTOs
{
    public class AddProductDTO
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Available { get; set; }
        public int? VendorId { get; set; }
    }
}
