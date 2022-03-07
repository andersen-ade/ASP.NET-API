using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAppWebAPI.Models.DTOs
{
    public class AddCustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
