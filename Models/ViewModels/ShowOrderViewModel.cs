
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShowOrderViewModel
    {
        public int OrderId{ get; set; }
        public int Count{ get; set; }
        public string ImageName { get; set; }
        public int OrderDetailId { get; set; }
        public int price { get; set; }
        public int sum { get; set; }
        public string Title { get; set; }
        public string CreateDate{ get; set; }

    }
}
