using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Models;
using Operations.FileOperation;

namespace BaruchApi.Controllers
{
    [Authorize(Roles = "admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly FileOp _file;

        public ArticlesController(ApplicationDbContext context, FileOp file)
        {
            _file = file;
            _context = context;
        }
        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
          if (_context.Articles == null)
          {
              return NotFound();
          }
            return await _context.Articles.ToListAsync();
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
          if (_context.Articles == null)
          {
              return NotFound();
          }
            var article = await _context.Articles.Include(o=>o.Comments).ThenInclude(c=>c.User.UserName).Include(p => p.labels).SingleOrDefaultAsync(i => i.Id == id);

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
          if (_context.Articles == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
          }
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.Id }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            if (_context.Articles == null)
            {
                return NotFound();
            }
            var article = await _context.Articles.Include(o => o.Comments).SingleOrDefaultAsync(i => i.Id == id);
            if (article == null)
            {
                return NotFound();
            }
            _context.ArticleComments.RemoveRange(article.Comments.ToList());
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int id)
        {
            return (_context.Articles?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("Uploadfile")]
        public async Task<IActionResult> File()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Request;

            if (httpRequest.Form.Files.Count > 0)
            {

              return Ok(await _file.UploadSingleFile(httpRequest.Form.Files[0]));
               
            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete("Deletefile/{filename}")]
        public async Task<IActionResult> dFile(string filename)
        {
            if (await _file.DeleteFile(filename))
                return Ok();
            else
                return NotFound();


        }
    }
}
