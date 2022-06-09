using Data.D;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baruch
{
    public class SubCategoryRepository : ISubcategory
    {

        private readonly ApplicationDbContext _ctx;


        public SubCategoryRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        public Task<SubCategory> Add(SubCategory subCategory)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<SubCategory> Edit(SubCategory subCategory)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SubCategory>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<SubCategory> GetById(int id)
        {
            try
            {
                var sc = await _ctx.subCategories.Include(c => c.products).ThenInclude(p=>p.OrderDetails).ThenInclude(o=>o.Order).SingleOrDefaultAsync(p => p.ID == id);


                return sc;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
