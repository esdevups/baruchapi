using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ViewModels;
namespace Baruch
{
    public interface IProductComments
    {
        public Task<ProductComment> Addcomment(ProductCommentViewModel comment);
        public Task<ProductComment> Editcomment(ProductCommentViewModel comment);
        public Task<bool> Deletecomment(int id);
        public Task<ProductComment> Getcomment(int id);
    }
}
