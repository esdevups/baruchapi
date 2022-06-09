using Mesfo.Internal_Servises.Paging;
using Models;

namespace Mesfo.Internal_Servises.ViewModels
{
    public class ArticleViewModel
    {
        public List<Article> Articles { get; set; }
        public PagingInfo paging { get; set; }
    }
}
