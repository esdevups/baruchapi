using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using Data;
using Microsoft.AspNetCore.Authorization;
using Operations.FileOperation;

namespace BaruchApi.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FileOp _file;

        public ProductsController(ApplicationDbContext context,FileOp file)
        {
            _file = file;
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.Include(p=>p.productProps).Include(c => c.Comments).ThenInclude(c => c.User.UserName).Include(p => p.labels).SingleOrDefaultAsync(i => i.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductViewModle productv)
        {
            if (id != productv.Id)
            {
                return BadRequest();
            }
            var product = new Product()
            {
                Title = productv.Title,
                CreateDate = productv.CreateDate,
                Id = productv.Id,
                ImageName = productv.ImageName,
                Price = productv.Price,
                Quantity = productv.Quantity,
                SubCategoryId = productv.SubCategoryId ,
                Text = productv.Text,
            };
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
        public async Task<ActionResult<Product>> PostProduct(ProductViewModle productv)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            var product = new Product()
            {
                Title = productv.Title,
                CreateDate = DateTime.Now,
                Id = productv.Id,
                ImageName = productv.ImageName,
                Price = productv.Price,
                Quantity = productv.Quantity,
                SubCategoryId = productv.SubCategoryId,
                Text = productv.Text,
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ObjectResult(product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
          
            var product = await _context.Products.Include(c => c.Comments).Include(i=>i.Images).SingleOrDefaultAsync(i => i.Id == id);
            try
            {
              await  _file.DeleteSingleFile(product.ImageName);

                foreach(var image in product.Images)
                {
                    await _file.DeleteSingleFile(image.Name);
                }
                _context.Images.RemoveRange(product.Images);
               
            }
            catch (Exception)
            {

                throw;
            }
            if (product == null)
            {
                return NotFound();
            }
            _context.ProductComments.RemoveRange(product.Comments.ToList());
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost("Uploadfile/{Productid}")]
        public async Task<IActionResult> File(int Productid)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Request;
            if (httpRequest.Form.Files.Count > 0)
            {
                await _file.UploadFile(httpRequest.Form.Files, Productid);
                
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete("Deletefile/{filename}")]
        public async Task<IActionResult> dFile(string filename)
        {
            if (await _file.DeleteSingleFile(filename))
                return Ok();
            else
                return NotFound();
                

        }
    }
}
