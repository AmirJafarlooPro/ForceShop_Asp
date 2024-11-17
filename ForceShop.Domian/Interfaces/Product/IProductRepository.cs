using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Order;

namespace ForceShop.Domian.Interfaces.Product
{
    public interface IProductRepository
    {
        Task<List<Models.Product.Product>> ProductsList();
        Task<List<Models.Product.Product>> GetLastProducts(int takeNum);
        Task<List<Models.Product.Product>> ShowProductsByGroup(int GroupID);
        Task<List<Models.Product.Product>> GetProductsByContains(string Contains);
        Task<Models.Product.Product?> GetProductByID(int ProductID);

        Task<int> GetOrderCountByUserID(int UserID);

        void UpdateOrderDetail(OrderDetail orderDetail);

        Task<Order> GetOrderByUserIDAsync (int UserID);

        void DeleteOrderDetail(OrderDetail orderDetail);

        decimal? GetProductPriceByID(int ProductID);
        Task<Order?> GetOrderByUserId(int userId);

        Task<bool> AddOrder(Order order);
        Task<bool> AddOrderDetail(OrderDetail order);

        Task<OrderDetail?> GetorderDetail(int OrderID, int ProductID);

        Task SaveChangesAsync();


    }
}
