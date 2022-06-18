using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ViewModels
{
    public class SubCategoryViewModle
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "عنوان را وارد کنید"), MaxLength(45, ErrorMessage = "دسته بندی تنها میتواند 45 کاراکتر باشد")]

        public string Title { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
    }
}
