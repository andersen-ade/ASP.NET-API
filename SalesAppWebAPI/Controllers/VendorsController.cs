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
    public class VendorsController : ControllerBase
    {
        private readonly CustomerDBContext _context;

        public VendorsController(CustomerDBContext context)
        {
            _context = context;
        }

        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            return await _context.Vendors.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("GetVendorDetails/{id}")]
        public async Task<ActionResult<Vendor>> GetVendorDetails(int id)
        {
            // Eager Overloading
            //var vendor = _context.Vendors
            //                            .Include(ven => ven.Products)
            //                                .ThenInclude(prod => prod.Sales)
            //                            .Where(ven => ven.Id == id)
            //                            .FirstOrDefault();

            //Explicit Loading
            var vendor = await _context.Vendors.SingleAsync(ven => ven.Id == id);

            _context.Entry(vendor)
                    .Collection(ven => ven.Products)
                    .Query()
                    .Include(prod => prod.Sales)
                    .Load();

            //Loading one to one r/ship
            //var product = await _context.Products.SingleAsync(prod => prod.Id == 7);

            //_context.Entry(product)
            //        .Reference(prod => prod.Name)
            //        .Load();

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor)
        {
            if (id != vendor.Id)
            {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorExists(id))
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
        [HttpGet("PostVendorDetails/{id}")]
        public async Task<ActionResult<Vendor>> PostVendorDetails(AddVendorDTO addVendorDTO)
        {
            // how to create relational data
            var vendors = new Vendor();
            vendors.Name = "Louis Vuitton";
            vendors.Address = "New York City";
            vendors.Email = "LV@gmail.com";
            vendors.CreationTime = DateTime.Now;

            Product prod1 = new Product();
            prod1.Name = "LV Jacket";
            prod1.Code = "003";
            prod1.Available = true;
            prod1.CreationTime = DateTime.Now;

            Product prod2 = new Product();
            prod2.Name = "Air Jordan 30s";
            prod2.Code = "004";
            prod2.Available = true;
            prod2.CreationTime = DateTime.Now;

            Product prod3 = new Product();
            prod3.Name = "Air Force 1";
            prod3.Code = "005";
            prod3.Available = true;
            prod3.CreationTime = DateTime.Now;

            Sale sales1 = new Sale();
            sales1.Code = "002-A";
            sales1.Amount = 15000;
            sales1.CreationTime = DateTime.Now;

            Sale sales2 = new Sale();
            sales2.Code = "003-A";
            sales2.Amount = 10000;
            sales2.CreationTime = DateTime.Now;

            Sale sales3 = new Sale();
            sales3.Code = "004-A";
            sales3.Amount = 10000;
            sales3.CreationTime = DateTime.Now;


            prod1.Sales.Add(sales1);
            prod2.Sales.Add(sales2);
            prod3.Sales.Add(sales3);

            vendors.Products.Add(prod1);
            vendors.Products.Add(prod2);
            vendors.Products.Add(prod3);

            _context.Vendors.Add(vendors);
            _context.SaveChanges();

            // Eager Overloading
            var vendor = _context.Vendors
                                        .Include(ven => ven.Products)
                                            .ThenInclude(prod => prod.Sales)
                                        .Where(ven => ven.Id == vendors.Id)
                                        .FirstOrDefault();

            if (vendor == null)
            {
                return NotFound();
            }

            return vendor;
        }

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(AddVendorDTO addVendorDTO)
        {
            //Mapping the entities to each other
            _context.Vendors.Add(new Vendor { Name = addVendorDTO.Name, Address = addVendorDTO.Address, Email = addVendorDTO.Email, CreationTime = DateTime.Now });
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var vendor = _context.Vendors.Include(x => x.Products).ThenInclude(x => x.Sales).SingleOrDefault(x => x.Id == id);

            if (vendor == null)
            {
                return NotFound();
            }

            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.Id == id);
        }
    }
}
