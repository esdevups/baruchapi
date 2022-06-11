using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [Required]
        public int Sum { get; set; }

        
        public string Code { get; set; }

        public bool IsFinaly { get; set; }
        public bool IsSend { get; set; }

        public AppUser User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
     

    }
}
