using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Data.D;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Baruch.Website.Pages
{
  
    
   
    [Authorize]
    public class editProfileModel : PageModel
    {
        private ApplicationDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public editProfileModel(ApplicationDbContext db, UserManager<AppUser> user, SignInManager<AppUser> signInManager)
        {
            _db = db;
            _userManager = user;
            _signInManager = signInManager;
        }
        public AppUser Userl { get; set; }
        
        [BindProperty]
        public InputModel Input { get; set; }
        public async Task OnGet()
        {
            Userl = await _db.Users.SingleOrDefaultAsync(i => i.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            Input = new InputModel()
            {
                Email = Userl.Email,
             
                Adress = Userl.Address,
                Phone = Userl.PhoneNumber,
                Postalcode = Userl.Postalcode,
                
            };


        }
        public async Task<IActionResult> OnPost()
        {
           
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Userl = await _db.Users.SingleOrDefaultAsync(i => i.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));


            if (Userl is null)
                return NotFound();
            Userl.Address = Input.Adress;
          
            Userl.PhoneNumber = Input.Phone;
            Userl.Email = Input.Email;
            Userl.Postalcode = Input.Postalcode;
            Userl.UserName = Userl.UserName;

            await _userManager.UpdateAsync(Userl);

            await _signInManager.SignOutAsync();
            return Redirect($"/identity/account/login");
        }

        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            ///

       
            [Required(ErrorMessage = "نام و نام خوانوادگی الزامی است")]

            [Display(Name = "Email")]
            public string Email { get; set; }



           

           
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>


            [Required(ErrorMessage = "تلفن الزامی است.")]
            public string Phone { get; set; }


            [Required(ErrorMessage = "آدرس الزامی است.")]
            public string Adress { get; set; }
            [Required(ErrorMessage = "کد پستی الزامی است")]

            public string Postalcode { get; set; }
        }
    }
}
