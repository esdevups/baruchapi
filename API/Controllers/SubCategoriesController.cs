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
    public class SubCategoriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FileOp _file;

        public SubCategoriesController(ApplicationDbContext context, FileOp file)
        {
            _file = file;
            _context = context;
        }







        // GET: api/SubCategories
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategory>>> GetsubCategories()
        {
          if (_context.subCategories == null)
          {
              return NotFound();
          }
            return await _context.subCategories.ToListAsync();
        }

        // GET: api/SubCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SubCategory>> GetSubCategory(int id)
        {
          if (_context.subCategories == null)
          {
              return NotFound();
          }
            var subCategory = await _context.subCategories.FindAsync(id);

            if (subCategory == null)
            {
                return NotFound();
            }

            return subCategory;
        }

        // PUT: api/SubCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubCategory(int id, SubCategoryViewModle subCategory)
        {
            if (id != subCategory.ID)
            {
                return BadRequest();
            }
            var subcat = new SubCategory()
            {
                ID = subCategory.ID,
                Title = subCategory.Title,
                CategoryID = subCategory.CategoryID
            };
        
            _context.Entry(subcat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubCategoryExists(id))
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

        // POST: api/SubCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SubCategory>> PostSubCategory(SubCategoryViewModle subCategory)
        {
          if (_context.subCategories == null)
          {
              return Problem("Entity set 'ApplicationDbContext.subCategories'  is null.");
          }
            var subcat = new SubCategory()
            {
               
                Title = subCategory.Title,
                CategoryID = subCategory.CategoryID
            };
            _context.subCategories.Add(subcat);
            await _context.SaveChangesAsync();

            return new ObjectResult(subcat);
        }

        // DELETE: api/SubCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            if (_context.subCategories == null)
            {
                return NotFound();
            }
            var subCategory = await _context.subCategories.Include(p=>p.products).ThenInclude(p=>p.Images).SingleOrDefaultAsync(i=>i.ID == id);
            if (subCategory == null)
            {
                return NotFound();
            }
            try
            {
                foreach (var product in subCategory.products)
                {
                    foreach (var img in product.Images)
                    {
                        await _file.DeleteFile(img.Name);
                        _context.Images.RemoveRange(product.Images);
                        _context.Products.Remove(product);
                    }
                }
                
               
                

            }
            catch (Exception)
            {

                throw;
            }
          

            _context.subCategories.Remove(subCategory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubCategoryExists(int id)
        {
            return (_context.subCategories?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
