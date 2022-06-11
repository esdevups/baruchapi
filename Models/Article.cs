using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Article
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]

        public string Title { get; set; }
        [Required]
        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Text { get; set; }
        public List<ArticleComment> Comments { get; set; }

        public List<ArticleLabel> labels { get; set; }
        

    }
}
