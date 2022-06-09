using Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Data.D;

namespace Baruch
{
    public class ProducRepository : IProduct, IBase, IProductComments
    {
        private readonly ApplicationDbContext _ctx;


        public ProducRepository(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Product> Add(Product product)
        {
            try
            {
               
                await _ctx.Products.AddAsync(product);
                await DbSaver();
                return product;
            }
            catch (Exception)
            {

                return null;
            }

        }



        public async Task<bool> Delete(int id)
        {
            try
            {

                var product = await _ctx.Products.Include(i => i.Images).SingleOrDefaultAsync(i => i.Id == id);
                foreach (var image in product.Images)
                {

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(),
                       "wwwroot/images", image.Name);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    _ctx.Images.Remove(image);
                }
                _ctx.Products.Remove(product);
                await DbSaver();
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }

        public async Task<Product> Edit(Product product)
        {
            try
            {
               
                
                _ctx.Attach(product).State = EntityState.Modified;
                await DbSaver();
                return product;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<IEnumerable<Product>> GetAll()
        {
           
                return await _ctx.Products.Include(p => p.OrderDetails).ToListAsync();
            
      
        }

        public async Task<Product> GetById(int id)
        {
            try
            {
                var product = await _ctx.Products.Include(c=>c.subCategory).Include(p => p.Comments).ThenInclude(p => p.User).Include(p => p.Images).SingleOrDefaultAsync(p => p.Id == id);
               

                return product;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public async Task DbSaver()
        {
            await _ctx.SaveChangesAsync();
        }

        public async Task<ProductComment> Addcomment(ProductCommentViewModel comment)
        {
            try
            {
                var com = new ProductComment()
                {
                    Text = comment.Text,

                    userid = comment.userid,
                    Productid = comment.productid
                };
                await _ctx.ProductComments.AddAsync(com);
                await DbSaver();
                return com;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ProductComment> Editcomment(ProductCommentViewModel comment)
        {
            try
            {
                var com = new ProductComment()
                {
                    Text = comment.Text,

                    userid = comment.userid,
                    Productid = comment.productid
                };
                _ctx.Attach(com).State = EntityState.Modified;
                await DbSaver();
                return com;

            }
            catch (Exception)
            {

                throw;
            }

        }

        async Task<bool> IProductComments.Deletecomment(int id)
        {
            try
            {

                var comment = await _ctx.ProductComments.FindAsync(id);
                _ctx.Remove(comment);
                await DbSaver();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<ProductComment> Getcomment(int id)
        {
            try
            {

                var comment = await _ctx.ProductComments.FindAsync(id);


                return comment;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
