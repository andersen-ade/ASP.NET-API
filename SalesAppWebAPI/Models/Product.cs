using System;
using System.Collections.Generic;

#nullable disable

namespace SalesAppWebAPI.Models
{
    public partial class Product
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool Available { get; set; }
        public int? VendorId { get; set; }
        public DateTime? CreationTime { get; set; }

        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
