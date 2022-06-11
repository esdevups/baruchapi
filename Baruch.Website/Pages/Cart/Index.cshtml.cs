﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;
using Servises;
using System.Security.Claims;
using ZarinpalSandbox;
using Data.D;
namespace Mesfo.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private CartOperations _cart;
        private ApplicationDbContext _ctx;

        public IndexModel(CartOperations cart, ApplicationDbContext ctx)
        {
            _cart = cart;
            _ctx = ctx;
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

        public async Task<IActionResult> OnPostPayment()
        {
            var order = _cart.GetOrder(User.FindFirstValue(ClaimTypes.NameIdentifier)).First();
            if (order == null)
            {
                return NotFound();
            }
            var payment = new Payment(order.sum);
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.OrderId}",
                "https://localhost:7183/Onlinepayment?id=" + order.OrderId, User.Identity.Name,_ctx.Users.Find(User.FindFirstValue(ClaimTypes.NameIdentifier)).PhoneNumber);
            if (res.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                return BadRequest();
            }

            return null;
            
        }
    }
}
