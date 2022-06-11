using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticleComment
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Text { get; set; }
        [ForeignKey("Article")]
        public int Productid { get; set; }
        [ForeignKey("User")]

        public string userid { get; set; }

        public Article Article{ get; set; }
        public AppUser User { get; set; }
        public DateTime Date{ get; set; }
    }
}
