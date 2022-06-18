using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ArticleViewModel
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]

        public string Title { get; set; }
        [Required]
        public string ImageName { get; set; }

        public DateTime CreateDate { get; set; }

        [Required]
        public string Text { get; set; }
    }
}
