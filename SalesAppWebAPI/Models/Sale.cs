using System;
using System.Collections.Generic;

#nullable disable

namespace SalesAppWebAPI.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
        public DateTime? CreationTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }


    }
}
