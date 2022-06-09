using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserViewModel
    {
      

        [Required, MaxLength(50)]

        public string FirstName { get; set; }
        [Required, MaxLength(50)]
      
        public string LastName { get; set; }
        [Required, MaxLength(50)]
     
        public string Email { get; set; }
        [Required, MaxLength(11)]
    
        public string Phone { get; set; }
        [Required,MinLength(8),MaxLength(250)]
        public string Password { get; set; }

        [Required, MaxLength(400)]
       
        public string Address { get; set; }
    }
}
