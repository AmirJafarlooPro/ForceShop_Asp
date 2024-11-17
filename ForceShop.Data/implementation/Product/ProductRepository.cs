using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Data.Contex;
using ForceShop.Domian.Interfaces.Product;
using ForceShop.Domian.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace ForceShop.Data.implementation.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly ForceShopContex _contex;
        public ProductRepository(ForceShopContex contex)
        {
            _contex = contex;
        }

        public async Task<bool> AddOrder(Order order)
        {
            try
            {
                await _contex.Orders.AddAsync(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddOrderDetail(OrderDetail order)
        {
            try
            {
                await _contex.OrderDetails.AddAsync(order);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Domian.Models.Product.Product>> GetLastProducts(int takeNum)
        {


            return await _contex.Products.Where(p => p.IsDelete == false).Include(p => p.ProductImages).OrderByDescending(p => p.CreateDate)
                .Take(takeNum).ToListAsync();
        }

        public async Task<Order?> GetOrderByUserId(int userId)
        {
            return await _contex.Orders.AsNoTracking().FirstOrDefaultAsync(p => p.UserID == userId);
        }

        public async Task<OrderDetail?> GetorderDetail(int OrderID, int ProductID)
        {
            return await _contex.OrderDetails.FirstOrDefaultAsync(p => p.OrderID == OrderID && p.ProductID == ProductID);
        }

        public async Task<Order> GetOrderByUserIDAsync(int UserID)
        {
            //return await _contex.Orders
            //    .Include(p => p.OrderDetails)
            //    .ThenInclude(od => od.Product)
            //    .ThenInclude(p => p.ProductImages)
            //    .Where(o => o.UserID == UserID && !o.IsFinally && o.OrderDetails.Any(od => !od.Product.IsDelete))
            //    .FirstOrDefaultAsync();

            return await _contex.Orders.Include(p => p.OrderDetails).ThenInclude(p => p.Product).ThenInclude(p => p.ProductImages).FirstOrDefaultAsync(p => p.UserID == UserID && !p.IsFinally);
        }

        public async Task<Domian.Models.Product.Product?> GetProductByID(int ProductID)
        {
            return await _contex.Products.Where(p => p.IsDelete == false).Include(p => p.ProductGroup).Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.ID == ProductID);
        }

        public decimal? GetProductPriceByID(int ProductID)
        {
            return _contex.Products.Find(ProductID).Price;
        }

        public async Task<List<Domian.Models.Product.Product>> GetProductsByContains(string Contains)
        {
            return await _contex.Products.Where(p => p.IsDelete == false).Include(p => p.ProductImages).Include(p => p.ProductGroup).Where(p =>
                    p.Title.Contains(Contains) || p.ShortDescription.Contains(Contains) || p.Tags.Contains(Contains))
                .ToListAsync();
        }

        public async Task<List<Domian.Models.Product.Product>> ProductsList()
        {
            return await _contex.Products.Where(p => p.IsDelete == false).Include(p => p.ProductImages).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _contex.SaveChangesAsync();
        }

        public async Task<List<Domian.Models.Product.Product>> ShowProductsByGroup(int GroupID)
        {
            return await _contex.Products.Include(p => p.ProductImages).Include(p => p.ProductGroup).Where(p => p.GroupID == GroupID && p.IsDelete == false).ToListAsync();
        }

        public async Task<int> GetOrderCountByUserID(int UserID)
        {
            return await _contex.Orders.Include(p => p.OrderDetails).Where(p => p.UserID == UserID && !p.IsFinally)
                .SumAsync(p => p.OrderDetails.Count);
        }


        public void DeleteOrderDetail(OrderDetail orderDetail)
        {
            _contex.OrderDetails.Remove(orderDetail);
        }

        public void UpdateOrderDetail(OrderDetail orderDetail)
        {
            _contex.OrderDetails.Update(orderDetail);
        }
    }
}
