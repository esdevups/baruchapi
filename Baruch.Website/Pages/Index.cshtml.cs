using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;


namespace Baruch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProduct _product;

        public IndexModel(IProduct product)
        {
            _product = product;
        }
        public IEnumerable<Product> Products { get; set; }
        public async Task OnGet()
        {
          
            Products =   _product.GetAll().Result;
            
        }
    }
}
