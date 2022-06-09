using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductProps
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }
        [Required]
        [MaxLength(40)]
        public string Value { get; set; }

        [ForeignKey("subCategory")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
