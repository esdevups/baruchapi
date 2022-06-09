using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Servises;
using System.Security.Claims;

namespace Mesfo.Pages
{
        [Authorize()]

    public class AddTocartModel : PageModel
    {
        public CartOperations _CartOperation { get; set; }

        public AddTocartModel(CartOperations cartOperations)
        {
            _CartOperation = cartOperations;
        }
        public async Task<IActionResult> OnGet(int id)
        {
          await  _CartOperation.AddToCart(id, User.FindFirstValue(ClaimTypes.NameIdentifier), 1);

            return RedirectToPage("/Cart/Index");
        }
    }
}
