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
    public class ProductsController : ControllerBase
    {
        private readonly CustomerDBContext _context;

        public ProductsController(CustomerDBContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("GetProductDetails/{id}")]
        public async Task<ActionResult<Product>> GetProductDetails(int id)
        {
            // Eager Overloading
            //var vendor = _context.Vendors
            //                            .Include(ven => ven.Products)
            //                                .ThenInclude(prod => prod.Sales)
            //                            .Where(ven => ven.Id == id)
            //                            .FirstOrDefault();

            //Explicit Loading
            var product = await _context.Products.SingleAsync(prod => prod.Id == id);

            _context.Entry(product)
                    .Collection(x => x.Sales)
                    .Load();

            //Loading one to one r/ship
            //var product = await _context.Products.SingleAsync(prod => prod.Id == 7);

            //_context.Entry(product)
            //        .Reference(prod => prod.Name)
            //        .Load();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        
        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(AddProductDTO addProductDTO)
        {
            _context.Products.Add(new Product{ Name = addProductDTO.Name, VendorId = addProductDTO.VendorId, Available = addProductDTO.Available, CreationTime = DateTime.Now, Code = addProductDTO.Code});
            await _context.SaveChangesAsync();

            return Ok();
        }

        // GET: api/Vendors/5
        [HttpGet("PostVendorDetails")]
        public async Task<ActionResult<Product>> PostProductDetails(AddProductDTO addProductDTO)
        {
            // for better perfomance put await, and add async to FirstOrDefault to run asynchronous
            var product = await _context.Products
                                        .Include(ven => ven.Vendor)
                                        .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = _context.Products.Include(x => x.Sales).SingleOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
