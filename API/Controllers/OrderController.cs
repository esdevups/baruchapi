using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Servises;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly CartOperations _cart;

        public OrdersController(ApplicationDbContext context, CartOperations cart)
        {
            _context = context;
            _cart = cart;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            
            return await _context.Orders.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> Get(int id)
        {
            if (_context.Orders == null)
            {
                return NotFound();
            }
            return  new ObjectResult(await _context.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o=>o.OrderId == id));
        }

        [HttpPost("Send/{productid}/{PostCode}")]
        public async Task<IActionResult> Send(int productid, string PostCode)
        {

            try
            {
                var Order = await _context.Orders.FindAsync(productid);

                Order.Code = PostCode;
                Order.IsSend = true;
                _context.Entry(Order).State = EntityState.Modified;
                return Ok();

            }
            catch (Exception)
            {
                return NotFound();
            }

        }

        [HttpPut("count/{Detailid}/{type}")]
        public async Task<string> Asde(int Detailid,string type)
        {
         return await  _cart.IncreaseOrDecreaseThenumberOfProducts(Detailid, type, 1);
        }
    }
}
