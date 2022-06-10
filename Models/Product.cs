using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="نام کالا الزامی است")]
        [MaxLength(300,ErrorMessage ="نام کالا میتواند حداکثر 300 کاراکتر باشد")]
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

        public bool IsSend { get; set; }
        public string PostCode { get; set; }

        public SubCategory subCategory { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<ProductComment> Comments { get; set; }
        public List<Image> Images{ get; set; }
        public List<ProductProps> productProps{ get; set; }
    }
}
