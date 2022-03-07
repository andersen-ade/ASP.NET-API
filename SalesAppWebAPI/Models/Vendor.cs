using System;
using System.Collections.Generic;

#nullable disable

namespace SalesAppWebAPI.Models
{
    public partial class Vendor
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? CreationTime { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
