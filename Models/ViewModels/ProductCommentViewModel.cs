using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ProductCommentViewModel
    {
        [Required]
        public string Text{ get; set; }

        [Required]
        public string userid { get; set; }

        [Required]
        public int productid { get; set; }

    }
}
