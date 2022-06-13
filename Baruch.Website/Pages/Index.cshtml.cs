using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Data.D;
using Baruch;
using Mesfo.Internal_Servises.Utilities;
using Operations.Datetime;
namespace Baruch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;

       private readonly ApplicationDbContext _context;

        public IndexModel(IProduct product, ApplicationDbContext context)
        {
            _product = product;
            _context = context;
        }



        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<SubCategory> subCategories { get; set; }
        public async Task OnGet()
        {
          
            Products =   _product.GetAll().Result;
           
            
           Articles = _context.Articles.Take(6).ToList();

            subCategories = _context.subCategories.Take(5);
            
           





        }
    }
}
