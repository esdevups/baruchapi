using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArticleLabel
    {

        public int Id { get; set; }
        public string Text{ get; set; }
        public string Url { get; set; }
        [ForeignKey("article")]
        public int Articleid { get; set; }


        public Article article{ get; set; }
    }

    public class ProductLabel
    {

        public int Id { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }

        [ForeignKey("product")]
        public int productid { get; set; }


        public Product product{ get; set; }
    }
}
