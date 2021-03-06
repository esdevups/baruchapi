using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModels;
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
        public IActionResult Post([FromBody] LableViewModle lable)
        {
            var lable1 = new ArticleLabel()
            {
                Text = lable.Text,
                Url = lable.Url,
                Articleid = lable.Parentid
            };
            _ctx.ArticleLabels.Add(lable1);
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
