using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Domian.Models.Order;
using ForceShop.Domian.Models.Product;

namespace ForceShop.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<List<Product>> GetNewProductsAsync();

        Task DeleteOrderDetail(int UserID, int ProductID);
        Task DecreaseCountOrderDetail(int UserID, int ProductID);
        Task<List<Product>> GetProductsByGroupIDAsync(int groupId);

        Task<Product> ShowProductByIdAsync(int productId);

        Task<List<Product>> GetProductsBySearch(string searchContent);
        Task<int> GetOrderCountByUserID(int UserID);
        Task<Order> GetOrderByUserID(int UserID);
        Task AddToCart(int UserID, int ProductID);

    }
}
