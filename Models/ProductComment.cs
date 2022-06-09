﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Models
{
    public class ProductComment
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Text { get; set; }
        [ForeignKey("Product")]
        public int Productid { get; set; }
        [ForeignKey("User")]

        public string userid { get; set; }

        public Product Product { get; set; }
        public AppUser User { get; set; }
    }
}
