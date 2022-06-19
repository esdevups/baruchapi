using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.D;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Baruch.Website.Pages
{
    [Authorize]
    public class dashbordModel : PageModel
    {
        //inject ApplicationDbcontext
        private ApplicationDbContext _db;

        public dashbordModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public AppUser Userl { get; set; }
        public async Task OnGet()
        {
            Userl = await _db.Users.SingleOrDefaultAsync(i => i.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}
