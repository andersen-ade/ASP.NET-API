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
    public class SalesController : ControllerBase
    {
        private readonly CustomerDBContext _context;

        public SalesController(CustomerDBContext context)
        {
            _context = context;
        }

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        // GET: api/Sales/5
        //Returning all sales + customer + product
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(int id)
        {
            var sale = await _context.Sales.AsNoTracking().Include(cus => cus.Customer).Include(p => p.Product).FirstOrDefaultAsync(c => c.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, AddSalesDTO addSalesDTO)
        {
            //This is hard coding the value you want to replace 
            // Retrieve entity by id
            var sale = await _context.Sales.FindAsync(id);

            if (id != sale.Id)
            {
                return BadRequest();
            }
           
            // Make changes on sale
            sale.Amount = 4000;
            _context.Entry(sale).State = EntityState.Modified;

            // Save changes in database
            _context.SaveChanges();


            return Ok();
        }

        // GET: api/Vendors/5
        [HttpPost("PostSalesDetails")]
        public async Task<ActionResult<Sale>> PostSalesDetails(AddSalesDTO addSalesDTO)
        {
         
            // Eager Overloading
            // for better perfomance put await, and add async to FirstOrDefault to run asynchronous
            var sale = await _context.Sales
                                        .Include(ven => ven.Customer)
                                        .FirstOrDefaultAsync();

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(AddSalesDTO addSalesDTO)
        {
            // Adding Data Transfer Object. Hard coding the properties
            _context.Sales.Add(new Sale { CustomerId = addSalesDTO.CustomerId, Amount = addSalesDTO.Amount, ProductId = addSalesDTO.ProductId, CreationTime = DateTime.Now, Code = addSalesDTO.Code});
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var sale = _context.Sales.Include(x => x.Customer).SingleOrDefault(x => x.Id == id);

            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(int id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }

        //use DTOs 
    }
}
