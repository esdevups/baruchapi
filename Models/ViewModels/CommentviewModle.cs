using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CommentviewModle
    {
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Text { get; set; }
      
        public int parentid { get; set; }
     

        public string userid { get; set; }

        public DateTime Date{ get; set; }
    }
}
