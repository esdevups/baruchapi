using Microsoft.AspNetCore.Mvc;
using Models;
using Operations.FileOperation;

namespace API.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]

    public class UIController : Controller
    {
        private readonly ApplicationDbContext _ctx;
        private readonly FileOp _file;

        public UIController(ApplicationDbContext ctx, FileOp file)
        {
            _ctx = ctx;
            _file = file;
        }
        [AllowAnonymous]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return new ObjectResult(_ctx.ui.FirstOrDefault());
        }
        [HttpPut()]
        public async Task<IActionResult> put([FromBody] UI ui)
        {
            _ctx.ui.Attach(ui).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _ctx.SaveChanges();

            return Ok();
        }

        [HttpPost("Uploadfile/{num}")]
        public async Task<IActionResult> File(int num)
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Request;

            if (httpRequest.Form.Files.Count > 0)
            {
                var ui = _ctx.ui.FirstOrDefault();
                switch (num)
                {
                    case 1:
                       ui.Image1 = await _file.UploadSingleFile(httpRequest.Form.Files[0]);
                        break;
                    case 2:
                       ui.Image2 = await _file.UploadSingleFile(httpRequest.Form.Files[0]);
                        break;
                    case 3:
                        ui.Image3 = await _file.UploadSingleFile(httpRequest.Form.Files[0]);
                        break;

                }
                _ctx.ui.Attach(ui).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _ctx.SaveChanges();
                return Ok();

            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete("Deletefile/{filename}")]
        public async Task<IActionResult> dFile(string filename)
        {
            if (await _file.DeleteFile(filename))
                return Ok();
            else
                return NotFound();


        }
    }
}
