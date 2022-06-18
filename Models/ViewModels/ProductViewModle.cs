using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ViewModels
{
    public class ProductViewModle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "نام کالا الزامی است")]
        [MaxLength(300, ErrorMessage = "نام کالا میتواند حداکثر 300 کاراکتر باشد")]
        public string Title { get; set; }
        [Required(ErrorMessage = "توضیاحات الزامی است")]
        public string Text { get; set; }
        [Required(ErrorMessage = "وارد کردن قیمت الزامی است")]
        public int Price { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required(ErrorMessage = "وارد کردن تعداد موجود در انبار الزامی است")]

        public DateTime CreateDate { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("subCategory")]
        public int SubCategoryId { get; set; }
    }
}
