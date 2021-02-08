using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetIt.Models;

namespace VetIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<List<Product>> GetProductsAsync()
        {
            var result =  await _context.Products.ToListAsync();

            return result;
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

            return new OkObjectResult(product);
            //return product;
        }


        // GET: api/Products/ActiveProducts
        [HttpGet("ActiveProducts")]
        public async Task<ActionResult<List<Product>>> GetActiveAndNonDeletedProducts()
        {
            var product = await _context.Products.Where(x => (x.DeleteDate == null || x.DeleteDate > DateTime.Now)
                                                        && (x.InactiveDate == null || x.InactiveDate > DateTime.Now))
                                                    .OrderByDescending(o => o.CreateDate).ToListAsync();

            if(product?.Count == 0)
            {
                return NoContent();
            }

            return product;
        }

        [HttpGet("ActiveDangerousDrugs")]
        public async Task<ActionResult<List<Product>>> GetActiveDangerousDrugs()
        {
            var product = await _context.Products.Where(x => (x.DeleteDate == null || x.DeleteDate > DateTime.Now)
                                                        && (x.InactiveDate == null || x.InactiveDate > DateTime.Now)
                                                        && x.Dangerous == true).ToListAsync();

            if (product?.Count == 0)
            {
                return NoContent();
            }

            return product;
        }


        // PUT: api/Products/5
        [HttpPut("{id}/{description}")]
        public async Task<ActionResult<Product>> PutProduct(int id, string description)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return new BadRequestResult();
            }

            product.ProductDescription = description;

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

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
