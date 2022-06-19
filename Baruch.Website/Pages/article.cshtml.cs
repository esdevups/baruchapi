using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Data.D;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Baruch.Website.Pages
{
    public class articleModel : PageModel
    {
        private ApplicationDbContext _ctx;

        // Construction of constructors and injectors _ctx
        public articleModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public List<Article> Articles { get; set; }
        public Article articl{ get; set; }
        [BindProperty]
        public ArticleComment comment { get; set; }
        public async Task OnGet(int id)
        {

            articl = await _ctx.Articles.Include(o => o.Comments).ThenInclude(c=>c.User).Include(o => o.labels).SingleOrDefaultAsync(i => i.Id == id);
           
            Articles = _ctx.Articles.Include(o => o.Comments).Include(o => o.labels).ToList();
        }
        public async Task<IActionResult> OnPost(string editor1)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect($"/identity/account/login?ReturnUrl=/Product?id={comment.Productid}");
            }
            
            comment.Date = DateTime.Now;
            comment.userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            comment.Text = editor1;

            await _ctx.ArticleComments.AddAsync(comment);

            await _ctx.SaveChangesAsync();

            return Redirect($"/Article?id={comment.Productid}");




        }

    }
}
