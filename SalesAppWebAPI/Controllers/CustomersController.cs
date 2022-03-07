using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesAppWebAPI.Models;
using SalesAppWebAPI.Models.DTOs;

namespace SalesAppWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerDBContext _context;

        public CustomersController(CustomerDBContext context)
        {
            _context = context;
        }

        

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("GetCustomerDetails/{id}")]
        public async Task<ActionResult<Customer>> GetCustomerDetails(int id)
        {
            // Eager Overloading
            //var vendor = _context.Vendors
            //                            .Include(ven => ven.Products)
            //                                .ThenInclude(prod => prod.Sales)
            //                            .Where(ven => ven.Id == id)
            //                            .FirstOrDefault();

            //Explicit Loading
            var customer = await _context.Customers.SingleAsync(cus => cus.Id == id);

            _context.Entry(customer)
                    .Collection(ven => ven.Sales)
                    .Query()
                    .Include(sal => sal.Product)
                    .Load();

            //Loading one to one r/ship
            //var product = await _context.Products.SingleAsync(prod => prod.Id == 7);

            //_context.Entry(product)
            //        .Reference(prod => prod.Name)
            //        .Load();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        // GET: api/Vendors/5
        [HttpGet("PostCustomerDetails/{id}")]
        public async Task<ActionResult<Customer>> PostCustomerDetails()
        {
            // how to create relational data
            var customers = new Customer();
            customers.FirstName = "Bryan";
            customers.LastName = "Chukwu";
            customers.Gender = "Male";
            customers.Email = "LV@gmail.com";
            customers.CreationTime = DateTime.Now;


            Sale sales1 = new Sale();
            sales1.Code = "002-A";
            sales1.Amount = 15000;
            sales1.CreationTime = DateTime.Now;

            Sale sales2 = new Sale();
            sales2.Code = "003-A";
            sales2.Amount = 10000;
            sales2.CreationTime = DateTime.Now;


            customers.Sales.Add(sales1);
            customers.Sales.Add(sales2);

            _context.Customers.Add(customers);
            _context.SaveChanges();

            // Eager Overloading
            var customer = _context.Customers
                                        .Include(ven => ven.Sales)
                                            .ThenInclude(sal => sal.Product)
                                        .FirstOrDefault();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }


        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(AddCustomerDTO addCustomerDTO)
        {
            _context.Customers.Add(new Customer { FirstName = addCustomerDTO.FirstName, LastName = addCustomerDTO.LastName, Email = addCustomerDTO.Email, Gender = addCustomerDTO.Gender, CreationTime = DateTime.Now });
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            //var customer = await _context.Customers.FindAsync(id);

            var customer = _context.Customers.Include(x => x.Sales).ThenInclude(x => x.Product).SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
