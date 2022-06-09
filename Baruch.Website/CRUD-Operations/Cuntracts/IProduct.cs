using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baruch
{
    public interface IProduct
    {
        public Task<IEnumerable<Product>> GetAll();
        public Task<Product> GetById(int id);
        public Task<Product> Add(Product product);
        public Task<Product> Edit(Product product);
        public Task<bool> Delete(int id);
    }
}
