﻿using System;
using System.Collections.Generic;

#nullable disable

namespace SalesAppWebAPI.Models
{
    public partial class Customer
    {
    
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime? CreationTime { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
