using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
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
        public async Task<IActionResult> PutArticle(int id, ArticleViewModel article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }
            //Criate article vaiable article1 to store the article
            var article1 = new Article()
            {
                Id = article.Id,
                Title = article.Title,
                ImageName = article.ImageName,
                CreateDate = article.CreateDate,
                Text = article.Text,
               
            };
            _context.Entry(article1).State = EntityState.Modified;

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
        public async Task<ActionResult<Article>> PostArticle(ArticleViewModel article)
        {
          if (_context.Articles == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Articles'  is null.");
          }
            var article1 = new Article()
            {
                Id = article.Id,
                Title = article.Title,
                ImageName = article.ImageName,
                CreateDate = DateTime.Now,
                Text = article.Text,

            };
            _context.Articles.Add(article1);
            await _context.SaveChangesAsync();

            return new ObjectResult(article1);
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

              return Ok(new {imagename = await _file.UploadSingleFile(httpRequest.Form.Files[0]) });
               
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
