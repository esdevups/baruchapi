using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class AppUser:IdentityUser
    {
        

   
        [MaxLength(400)]
        public string Address{ get; set; }
        public string ProfileImageName{ get; set; }
        public string Postalcode{ get; set; }
        public ICollection<Order> Orders{ get; set; }
        public ICollection<ProductComment> ProductComments{ get; set; }
        public ICollection<ArticleComment> ArticleComments{ get; set; }

    }
}
