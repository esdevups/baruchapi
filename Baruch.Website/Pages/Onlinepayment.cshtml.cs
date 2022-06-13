using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.D;
using Microsoft.EntityFrameworkCore;
using Zarinpal;

namespace Baruch.Website.Pages
{
    public class OnlinepaymentModel : PageModel
    {
        private ApplicationDbContext _ctx;

        public OnlinepaymentModel(ApplicationDbContext ctx)
        {
        
            _ctx = ctx;
        }
        public bool issucsses{ get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();
                var order = await _ctx.Orders.Include(o=>o.OrderDetails).ThenInclude(o=>o.Product).SingleOrDefaultAsync(i=>i.OrderId == id);
                var payment = new Payment("8e76f8b5-e7d6-451e-8014-2d8fb024ba09", order.Sum);
                var res = payment.Verification(authority).Result;
                if (res.Status == 100)
                {
                    order.IsFinaly = true;
                    foreach (var detail in order.OrderDetails)
                    {
                        var product = detail.Product;

                        if(product.Quantity >= detail.Count)
                        {
                            product.Quantity -= detail.Count;

                        }
                        _ctx.Products.Update(product);
                    }
                    _ctx.Orders.Update(order);
                    _ctx.SaveChanges();
                    issucsses = true;
                    return Page();
                }
                else
                {
                    issucsses = false;

                }

            }
            else
            {
                issucsses = false;
                return Page();

            }

            return NotFound();
        }
    }
}
