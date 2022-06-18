using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.D;
using System.Security.Claims;

namespace Baruch.Website.Pages
{
    public class productModel : PageModel
    {


        private  ApplicationDbContext _db;
        private IProduct _pr;

        public productModel(ApplicationDbContext db, IProduct pr)
        {
            _db = db;
            _pr = pr;
        }




        public Product product { get; set; }
        public bool Issucsses { get; set; }
        [BindProperty]
        public ProductComment comment { get; set; } 

        public async Task  OnGet(int id)
        {
            product = await _pr.GetById(id);
           
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
         
            await _db.ProductComments.AddAsync(comment);

               await _db.SaveChangesAsync();
            return RedirectToPage($"Product?id={comment.Productid}");




        }
    }
}
