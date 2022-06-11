using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.ViewModels;
using Models;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private ApplicationDbContext _ctx;



        public UsersController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpGet]
        public IActionResult gets()
        {
            var list = new List<UserViewModel> ();

            foreach (var user in _ctx.Users.ToList()) 
            {
                var userv = new UserViewModel
                {
                    id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Address = user.Address,
                };

                list.Add(userv);
                    
            }
            return new ObjectResult(list);
        }
        [HttpGet("{id}")]
        public IActionResult getbyid(string id)
        {
            
            return new ObjectResult(_ctx.Users.Find(id));
        }
    }
}
