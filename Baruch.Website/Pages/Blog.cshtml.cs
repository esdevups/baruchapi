using Baruch;
using Mesfo.Internal_Servises.Utilities;
using Mesfo.Internal_Servises.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using Data.D;
namespace Baruch.Website.Pages
{
    public class BlogModel : PageModel
    {
        //inject ApplicationDbContext
        private readonly ApplicationDbContext _ctx;

        public BlogModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public ArticleViewModel PagingViewModel { get; set; }
        public List<Article> articles { get; set; }
        public string Selecttag { get; set; }
        public string AsDs { get; set; }
        public async Task<IActionResult> OnGet( int pageId = 1, string Filter = "Date", string type = "asc")
        {
            Selecttag = Filter;
            AsDs = type;

            PagingViewModel = new ArticleViewModel()
            {
                Articles = _ctx.Articles.ToList(),
                
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Blog?pageId=:");

            param.Append("&Filter=");
            if (Filter != null)
                param.Append(Filter);

            switch (Filter)
            {
                case "Date":
                    if (type == "asc")
                    {
                        PagingViewModel.Articles = _ctx.Articles
                    .OrderBy(o => o.CreateDate).ToList();
                    }
                    else
                    {
                        PagingViewModel.Articles = _ctx.Articles
                     .OrderByDescending(o => o.CreateDate).ToList();
                    }

                    break;
               
            }
            var count = PagingViewModel.Articles.Count;
            PagingViewModel.paging = new PagingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = SD.Pagingcuont,
                TotalItems = count,
                UrlParam = param.ToString()
            };
            PagingViewModel.Articles = PagingViewModel.Articles
                .Skip((pageId - 1) * SD.Pagingcuont)
                .Take(SD.Pagingcuont).ToList();
            return Page();
        }
    }
}
