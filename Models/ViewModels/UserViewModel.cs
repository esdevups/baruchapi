using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserViewModel
    {

        public string id{ get; set; }
        [Required, MaxLength(50)]

        public string UserName { get; set; }
    
        [Required, MaxLength(50)]
     
        public string Email { get; set; }
        [Required]
    
        public string Phone { get; set; }
        [Required]
        public string Password { get; set; }

        [Required, MaxLength(400)]
       
        public string Address { get; set; }
    }
}
