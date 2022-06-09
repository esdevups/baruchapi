using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baruch
{
    public interface ISubcategory
    {
        public Task<IEnumerable<SubCategory>> GetAll();
        public Task<SubCategory> GetById(int id);
        public Task<SubCategory> Add(SubCategory subCategory);
        public Task<SubCategory> Edit(SubCategory subCategory);
        public Task<bool> Delete(int id);
    }
}
