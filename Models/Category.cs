using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Category
    {
        public int Id{ get; set; }

        [Required(ErrorMessage ="عنوان را وارد کنید"),MaxLength(45,ErrorMessage ="دسته بندی تنها میتواند 45 کاراکتر باشد")]
        public string Title{ get; set; }

        public ICollection<SubCategory>subCategories { get; set; }
    }
}
