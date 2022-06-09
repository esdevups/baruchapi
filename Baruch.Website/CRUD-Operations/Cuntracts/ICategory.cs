using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baruch
{
    public interface ICategory
    {
        public Task<Category> Add(Category category);
        public Task<bool> Delete(int id);
        public Task<Category> Edit(Category category);
        public Task<IEnumerable<Category>> GetAll();
        public Task<Category> GetByID(int id);
    }
}
