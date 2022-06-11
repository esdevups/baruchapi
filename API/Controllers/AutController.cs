using Data;
using Data.D;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Models;
namespace Mesfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutController : ControllerBase
    {
        private readonly ApplicationDbContext _ctx;
        public AutController(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel lv)
        {
         
            if (await _ctx.Admins.AnyAsync(u => u.Email.ToLower() == lv.email.ToLower()))
            {
                var user = _ctx.Admins.SingleOrDefaultAsync(u => u.Email == lv.email).Result;
                if (lv.email.ToLower() == user.Email.ToLower() && lv.Password == user.Password)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTopLearn"));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var tokenOption = new JwtSecurityToken(
                        issuer: "https://localhost:7218/",
                        claims: new List<Claim>
                        {
                    new Claim("Name",user.FirstName),
                    new Claim("Email",user.Email),
                    new Claim("Id",user.Id),
                    new Claim(ClaimTypes.Role,user.Role),
                    new Claim(ClaimTypes.NameIdentifier,user.Id)
                        },
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );

                    return Ok( new { token = new JwtSecurityTokenHandler().WriteToken(tokenOption) });
                }
                else
                {
                    return BadRequest("ایمیل یا پسورد اشتباه است");
                }
            }
            else
                return BadRequest("کاربری با این ایمیل یافت نشد");



        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody]Admin admin)
        {
            _ctx.Admins.Attach(admin).State = EntityState.Modified;
           await _ctx.SaveChangesAsync();

            return Ok();
           


        }

    }
}
