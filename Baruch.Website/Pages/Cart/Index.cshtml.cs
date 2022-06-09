using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using Servises;
using System.Security.Claims;

namespace Mesfo.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private CartOperations _cart;

        public IndexModel(CartOperations cart)
        {
            _cart = cart;
        }

        public List<ShowOrderViewModel> orders { get; set; }
        public async Task OnGet()
        {
            orders =  _cart.GetOrder(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public async Task<IActionResult> OnPostIncrement(int id)
        {
            await _cart.IncreaseOrDecreaseThenumberOfProducts(id, "increment", 1);

            return RedirectToPage("/cart/index");
        }
        public async Task<IActionResult> OnPostDecrement(int id)
        {
            await _cart.IncreaseOrDecreaseThenumberOfProducts(id, "decrement", 1);

            return RedirectToPage("/cart/index");
        }
    }
}
