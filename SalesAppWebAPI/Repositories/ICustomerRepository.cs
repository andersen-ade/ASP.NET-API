using SalesAppWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAppWebAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> Get();
        Task<Customer> Get(int id);
        Task<Customer> Create(Customer customer);
        Task Update(Customer customer);
        Task Delete(int id);
    }
}
