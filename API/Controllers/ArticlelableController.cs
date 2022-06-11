using Microsoft.AspNetCore.Mvc;
using Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Authorize(Roles = "admin")]

    [Route("api/[controller]")]
    [ApiController]
    public class ArticlelableController : ControllerBase
    {
        // GET: api/<ArticlelableController>

        private ApplicationDbContext _ctx;



        public ArticlelableController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }

        // POST api/<ArticlelableController>
        [HttpPost]
        public IActionResult Post([FromBody] ArticleLabel lable)
        {
            _ctx.ArticleLabels.Add(lable);
            _ctx.SaveChanges();

            return Ok();
        }

   

        // DELETE api/<ArticlelableController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var lable = _ctx.ArticleLabels.Find(id);
            _ctx.ArticleLabels.Remove(lable);

            _ctx.SaveChanges();
        }
    }
}
