using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.D;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BaruchApi.Controllers
{
    [Authorize(Roles = "admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ArticleCommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ArticleComments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticleComment>>> GetArticleComments()
        {
          if (_context.ArticleComments == null)
          {
              return NotFound();
          }
            return await _context.ArticleComments.ToListAsync();
        }

        // GET: api/ArticleComments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticleComment>> GetArticleComment(int id)
        {
          if (_context.ArticleComments == null)
          {
              return NotFound();
          }
            var articleComment = await _context.ArticleComments.FindAsync(id);

            if (articleComment == null)
            {
                return NotFound();
            }

            return articleComment;
        }

        // PUT: api/ArticleComments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticleComment(int id, ArticleComment articleComment)
        {
            if (id != articleComment.Id)
            {
                return BadRequest();
            }

            _context.Entry(articleComment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleCommentExists(id))
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

        // POST: api/ArticleComments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArticleComment>> PostArticleComment(ArticleComment articleComment)
        {
          if (_context.ArticleComments == null)
          {
              return Problem("Entity set 'ApplicationDbContext.ArticleComments'  is null.");
          }
            _context.ArticleComments.Add(articleComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticleComment", new { id = articleComment.Id }, articleComment);
        }

        // DELETE: api/ArticleComments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticleComment(int id)
        {
            if (_context.ArticleComments == null)
            {
                return NotFound();
            }
            var articleComment = await _context.ArticleComments.FindAsync(id);
            if (articleComment == null)
            {
                return NotFound();
            }

            _context.ArticleComments.Remove(articleComment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleCommentExists(int id)
        {
            return (_context.ArticleComments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
