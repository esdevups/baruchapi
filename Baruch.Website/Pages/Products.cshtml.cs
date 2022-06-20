using Baruch;
using Mesfo.Internal_Servises.Utilities;
using Mesfo.Internal_Servises.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Mesfo.Pages
{
    public class ProductsModel : PageModel
    {
        private  ISubcategory _ctx;

        public ProductsModel(ISubcategory ctx)
        {
            _ctx = ctx;
        }
        public ProductPagingViewModel PagingViewModel { get; set; }
        public List<Product>products { get; set; }
        public string Selecttag { get; set; }
        public string AsDs { get; set; }
        public async Task<IActionResult> OnGet(int Category = 1,int pageId = 1, string Filter= "Price",string type="asc")
        {
            Selecttag = Filter;
            AsDs = type;

            PagingViewModel = new ProductPagingViewModel()
            {
                products =  _ctx.GetById(Category).Result.products,

            };
            if (PagingViewModel.products.Count == 0)
                return Redirect("/");
            StringBuilder param = new StringBuilder();
            param.Append("/Products?pageId=:");

            param.Append("&Filter=");
            if (Filter != null)
                param.Append(Filter);

            switch (Filter)
            {
                case "Price":
                    if (type == "asc")
                    {
                        PagingViewModel.products = _ctx.GetById(Category).Result.products
                    .OrderByDescending(o => o.Price).ToList();
                    }
                    else
                    {
                        PagingViewModel.products = _ctx.GetById(Category).Result.products
                 .OrderBy(o => o.Price).ToList();
                    }

                    break;
                case "Sale":
                    if (type == "asc")
                    {
                        PagingViewModel.products = _ctx.GetById(Category).Result.products
                    .OrderBy(o => o.OrderDetails.Select(o=>o.Order.IsFinaly).Count()).ToList();
                    }
                    else
                    {
                        PagingViewModel.products = _ctx.GetById(Category).Result.products
                 .OrderByDescending(o => o.OrderDetails.Select(o => o.Order.IsFinaly).Count()).ToList();
                    }

                    break;
            }
            var count = PagingViewModel.products.Count;
            PagingViewModel.paging = new PagingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = SD.Pagingcuont,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            PagingViewModel.products = PagingViewModel.products
                .Skip((pageId - 1) * SD.Pagingcuont)
                .Take(SD.Pagingcuont).ToList();
            return Page();
        }
    }
}
