using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Image
    {
        [Key]
        public int Id{ get; set; }
        [Required]
        public string Name{ get; set; }

        [ForeignKey("product")]
        public int ProductId{ get; set; }

        public Product product{ get; set; }
    }
}
