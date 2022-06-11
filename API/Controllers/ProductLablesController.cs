using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
namespace API.Controllers
{
    [Authorize(Roles = "admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ProductLablesController : ControllerBase
    {
        private ApplicationDbContext _ctx;



        public ProductLablesController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // POST api/<ArticlelableController>
        [HttpPost]
        public IActionResult Post([FromBody] ProductLabel lable)
        {
            _ctx.ProductLabel.Add(lable);
            _ctx.SaveChanges();

            return Ok();
        }



        // DELETE api/<ArticlelableController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lable = _ctx.ProductLabel.Find(id);
            _ctx.ProductLabel.Remove(lable);

            _ctx.SaveChanges();
        }
    }
}
