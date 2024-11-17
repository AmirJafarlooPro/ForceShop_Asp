using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Domian.Interfaces.Product;
using ForceShop.Domian.Models.Order;
using ForceShop.Domian.Models.Product;

namespace ForceShop.Application.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task AddToCart(int UserID, int ProductID)
        {
            Order? order = await _productRepository.GetOrderByUserId(UserID);

            if (order == null)
            {
                order = new Order()
                {
                    UserID = UserID,
                    IsDelete = false,
                    CreateDate = DateTime.Now
                };
                var result = await _productRepository.AddOrder(order);
                if (result)
                {
                    await _productRepository.SaveChangesAsync();
                }

            }

            var OrderDetail = await _productRepository.GetorderDetail(order.ID, ProductID);

            if (OrderDetail != null)
            {
                OrderDetail.Count++;
            }
            else
            {
                OrderDetail = new OrderDetail()
                {
                    Count = 1,
                    ProductID = ProductID,
                    IsDelete = false,
                    CreateDate = DateTime.Now,
                    Price = _productRepository.GetProductPriceByID(ProductID).Value,
                    OrderID = order.ID
                };
                var result = await _productRepository.AddOrderDetail(OrderDetail);
            }
            await _productRepository.SaveChangesAsync();
        }

        public async Task DecreaseCountOrderDetail(int UserID, int ProductID)
        {
            var order = await _productRepository.GetOrderByUserIDAsync(UserID);
            if (order != null)
            {
                var orderDetail = await _productRepository.GetorderDetail(order.ID, ProductID);
                if (orderDetail != null)
                {
                    if (orderDetail.Count>1)
                    {
                        orderDetail.Count--;
                        _productRepository.UpdateOrderDetail(orderDetail);
                    }
                    else if (orderDetail.Count == 1)
                    {
                        _productRepository.DeleteOrderDetail(orderDetail);
                    }

                    await _productRepository.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteOrderDetail(int UserID, int ProductID)
        {
            var order = await _productRepository.GetOrderByUserIDAsync(UserID);
            if (order != null)
            {
                var orderDetail = await _productRepository.GetorderDetail(order.ID, ProductID);
                if (orderDetail != null)
                {
                    _productRepository.DeleteOrderDetail(orderDetail);
                    await _productRepository.SaveChangesAsync();
                }
            }
        }

        public async Task<List<Product>> GetNewProductsAsync()
        {
            return await _productRepository.GetLastProducts(10);
        }

        public async Task<Order> GetOrderByUserID(int UserID)
        {
            return await _productRepository.GetOrderByUserIDAsync(UserID);
        }

        public async Task<int> GetOrderCountByUserID(int UserID)
        {
            return await _productRepository.GetOrderCountByUserID(UserID);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _productRepository.ProductsList();
        }

        public async Task<List<Product>> GetProductsByGroupIDAsync(int groupId)
        {
            return await _productRepository.ShowProductsByGroup(groupId);
        }

        public async Task<List<Product>> GetProductsBySearch(string searchContent)
        {
            return await _productRepository.GetProductsByContains(searchContent);
        }

        public async Task<Product> ShowProductByIdAsync(int productId)
        {
            return await _productRepository.GetProductByID(productId);
        }
    }
}
