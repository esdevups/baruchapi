using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.ViewModels;
using Models;
using Data;

namespace BaruchApi.Controllers
{
    [Authorize(Roles = "admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductPropsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductPropsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductProps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductProps>>> GetproductProps()
        {
          if (_context.productProps == null)
          {
              return NotFound();
          }
            return await _context.productProps.ToListAsync();
        }

        // GET: api/ProductProps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductProps>> GetProductProps(int id)
        {
          if (_context.productProps == null)
          {
              return NotFound();
          }
            var productProps = await _context.productProps.FindAsync(id);

            if (productProps == null)
            {
                return NotFound();
            }

            return productProps;
        }

        // PUT: api/ProductProps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductProps(int id, PropsViewModle productProps)
        {
            if (id != productProps.Id)
            {
                return BadRequest();
            }
            var prop = new ProductProps()
            {
                Id = productProps.Id,
                Title = productProps.Title,
                Value = productProps.Value,
                ProductId = productProps.ProductId
            };
        
            _context.Entry(prop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductPropsExists(id))
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

        // POST: api/ProductProps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductProps>> PostProductProps(PropsViewModle productProps)
        {
          if (_context.productProps == null)
          {
              return Problem("Entity set 'ApplicationDbContext.productProps'  is null.");
          }
            var prop = new ProductProps()
            {
            
                Title = productProps.Title,
                Value = productProps.Value,
                ProductId = productProps.ProductId
            };
            _context.productProps.Add(prop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductProps", new { id = productProps.Id }, productProps);
        }

        // DELETE: api/ProductProps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductProps(int id)
        {
            if (_context.productProps == null)
            {
                return NotFound();
            }
            var productProps = await _context.productProps.FindAsync(id);
            if (productProps == null)
            {
                return NotFound();
            }

            _context.productProps.Remove(productProps);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductPropsExists(int id)
        {
            return (_context.productProps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
