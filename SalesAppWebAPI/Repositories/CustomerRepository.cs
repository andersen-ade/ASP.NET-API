using SalesAppWebAPI.Models;
using SalesAppWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesAppWebAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDBContext _customerDBContext;
        public CustomerRepository(CustomerDBContext customerDBContext)
        {
            _customerDBContext = customerDBContext;
        }

        public Task<Customer> Create(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerDBContext.Customers;
        }

        public Task Update(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}

