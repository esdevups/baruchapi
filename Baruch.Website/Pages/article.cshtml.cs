using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Data.D;
using Microsoft.EntityFrameworkCore;

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
        public async Task OnGet(int id)
        {

            articl = await _ctx.Articles.Include(o => o.Comments).Include(o => o.labels).SingleOrDefaultAsync(i => i.Id == id);
           
            Articles = _ctx.Articles.Include(o => o.Comments).Include(o => o.labels).ToList();
        }

    }
}
