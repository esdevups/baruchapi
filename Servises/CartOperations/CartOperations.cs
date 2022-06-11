using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.D;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.ViewModels;
using Operations.Datetime;

namespace Servises
{
    public class CartOperations
    {
        private ApplicationDbContext _ctx;



        public CartOperations(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<bool> AddToCart(int id, string userid, int count)
        {
            string CurrentUserID = userid;


            Order order = _ctx.Orders.SingleOrDefault(o => o.UserId == CurrentUserID && !o.IsFinaly);
            if (order == null)
            {
                order = new Order()
                {
                    UserId = CurrentUserID,
                    CreateDate = DateTime.Now,
                    IsFinaly = false,
                    Sum = 0,
                    Code = ""
                };
                _ctx.Orders.Add(order);
                await _ctx.SaveChangesAsync();
                if (!await IsExist(id, count))
                {
                    return false;
                }
                _ctx.OrderDetails.Add(new OrderDetail()
                {
                    OrderId = order.OrderId,
                    Count = count,
                    Price = _ctx.Products.Find(id).Price,
                    ProductId = id
                });
                _ctx.SaveChanges();
            }
            else
            {
                var details = _ctx.OrderDetails.SingleOrDefault(d => d.OrderId == order.OrderId && d.ProductId == id);
                if (details == null)
                {
                    if (!await IsExist(id, count))
                    {
                        return false;
                    }
                    _ctx.OrderDetails.Add(new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        Count = count,
                        Price = _ctx.Products.Find(id).Price,
                        ProductId = id
                    });
                }
                else
                {
                    if (!await IsExist(id, count + details.Count))
                    {
                        return false;
                    }
                    details.Price = _ctx.Products.Find(id).Price;
                    details.Count += count;
                    _ctx.Update(details);
                }

                _ctx.SaveChanges();
            }
            UpdateSumOrder(order.OrderId);
            return true;
        }
        public  void UpdateSumOrder(int orderId)
        {
            var order = _ctx.Orders.Find(orderId);
            var list = _ctx.OrderDetails.Where(o => o.OrderId == order.OrderId).ToList();
            var sum = 0;

            foreach (var item in list)
            {
                var price = item.Price * item.Count;

                sum += price;

                price = 0;
            }
            order.Sum = sum;
            _ctx.Update(order);
             _ctx.SaveChanges();
        }

        public List<ShowOrderViewModel> GetOrder(string userid)
        {
            Order order = _ctx.Orders.Include(o=>o.OrderDetails).SingleOrDefault(o => o.UserId == userid && !o.IsFinaly);
            if (order == null)
            {
                return null;
            }
            var list = _ctx.OrderDetails.Include(o => o.Product).Where(o => o.OrderId == order.OrderId).ToList();
            var viewmodlist = new List<ShowOrderViewModel>();

            foreach (var item in list)
            {
                viewmodlist.Add(new ShowOrderViewModel
                {
                    CreateDate = Persiandate.PersianDate(order.CreateDate),
                    OrderId = order.OrderId,
                    Count = item.Count,
                    ImageName = item.Product.ImageName,
                    OrderDetailId = item.OrderDetailID,
                    price = item.Price,
                    sum = item.Price * item.Count,
                    Title = item.Product.Title,

                });
            }
            return viewmodlist;

        }
        public async Task<bool> DeleteOrder(int orderid)
        {
            var order = await _ctx.Orders.Include(o => o.OrderDetails).SingleOrDefaultAsync(o => o.OrderId == orderid);
            _ctx.OrderDetails.RemoveRange(order.OrderDetails.ToList());
            _ctx.Orders.Remove(order);
            await _ctx.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteOrderDetail(int orderdid)
        {
            var orderd = await _ctx.OrderDetails.FindAsync(orderdid);
            _ctx.OrderDetails.Remove(orderd);
            await _ctx.SaveChangesAsync();
            UpdateSumOrder(orderd.OrderId);

            return true;

        }
        public async Task<bool> IsExist(int productid, int count)
        {
            var product = await _ctx.Products.FindAsync(productid);

            if (product.Quantity >= count)

                return true;
            else
                return false;
        }

        public async Task<string> IncreaseOrDecreaseThenumberOfProducts(int orderdetailid, string Operation, int Cunt)
        {
            try
            {
                var orderd = await _ctx.OrderDetails.Include(o=>o.Order).ThenInclude(o=>o.OrderDetails).Include(o=>o.Product).SingleOrDefaultAsync(o=>o.OrderDetailID == orderdetailid);

           
                var product = orderd.Product;
                switch (Operation)
                {
                    case "increment":
                        if (orderd.Count >= Cunt)
                        {
                            if (product.Quantity >= orderd.Count)
                            {
                                orderd.Count += Cunt;

                                _ctx.OrderDetails.Attach(orderd).State = EntityState.Modified;

                                await _ctx.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            return "این تعداد در انبار موجود نمی باشد";
                        }
                        break;
                    case "decrement":

                        if (orderd.Count >= Cunt)
                        {
                            orderd.Count -= Cunt;
                           
                            if (orderd.Count !=0)
                            {
                                _ctx.OrderDetails.Attach(orderd).State = EntityState.Modified;

                            }
                            else
                            {
                                _ctx.Remove(orderd);
                        
                            }
                    
                            await _ctx.SaveChangesAsync();
                            if (orderd.Order.OrderDetails is null)
                            {
                                _ctx.Orders.Remove(orderd.Order);
                               await _ctx.SaveChangesAsync();
                                    
                            }
                        }
                        break;


                }

                _ctx.OrderDetails.Attach(orderd).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return "عملیات با موفقیت انجام شد";
            }
            catch (Exception)
            {

                return "مشکلی رخ داد";
            }
         
        }


        public async Task<Order> getorderforuser(int id)
        {
            return await _ctx.Orders.FindAsync(id);
        }



        public async Task<OrderDetail> getorderdetailforuser(int id)
        {
            return await _ctx.OrderDetails.Include(o=>o.Order).SingleOrDefaultAsync(o=>o.OrderDetailID == id);
        }
    }
}
