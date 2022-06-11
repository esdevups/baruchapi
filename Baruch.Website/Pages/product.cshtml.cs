using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Baruch.Website.Pages
{
    public class productModel : PageModel
    {
        private IProduct _pr;
        public productModel(IProduct pr)
        {

            _pr = pr;
        }

        public Product product{ get; set; }

      
        public async Task OnGet(int id)
        {
            product = await _pr.GetById(id);
        }
    }
}
