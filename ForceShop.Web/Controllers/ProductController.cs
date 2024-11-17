using System.Security.Claims;
using ForceShop.Application.Extensions;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Domian.Models.Order;
using ForceShop.Domian.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ForceShop.Web.Controllers
{
    public class ProductController : Controller
    {
        #region Dependency Injection

        private readonly IProductService _productService;

        private readonly IUserService _userService;

        public ProductController(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        #endregion

        //--------------------------------------------

        #region Index

        public async Task<IActionResult> index(string SearchContent)
        {

            var products = await _productService.GetProductsBySearch(SearchContent);
            TempData["PageName"] = "جستجو";

            return View(products);

        }

        #endregion

        #region Group

        [HttpGet("Group")]
        public async Task<IActionResult> ShowProductByGroup(int id, string title)
        {

            var Products = await _productService.GetProductsByGroupIDAsync(id);
            TempData["PageName"] = title;

            return View(Products);
        }

        #endregion

        #region Product

        [HttpGet("Product/{id}")]
        public async Task<IActionResult> ShowProduct([FromRoute] int id)
        {
            var Product = await _productService.ShowProductByIdAsync(id);

            if (Product != null)
            {
                TempData["PageName"] = $"{Product.Title}";
                return View(Product);
            }

            return NotFound($"محصولی با ایدی {id} پیدا نشد !");
        }

        #endregion

        #region AddToCart

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            string emailUser = User.FindFirstValue(ClaimTypes.Email);

            int currentUserId = _userService.GetUserIDByEmail(emailUser);

            await _productService.AddToCart(currentUserId, id);

            TempData["AddToCartSuccess"] = "موفقیت";

            return RedirectToAction("ShowProduct", new { id = id });

        }

        #endregion

        #region ShowCart

        [Authorize]
        [HttpGet("/ShopCart")]
        public async Task<IActionResult> ShowShopCartOrders()
        {
            TempData["PageName"] = $"سبد خرید";
            int userId = User.GetUserId();
            var order = await _productService.GetOrderByUserID(userId);
            return View(order);
        }

        #endregion

        #region RemoveFromCart

        [Authorize]
        [HttpGet("RemoveCart/{productID}")]
        public async Task<IActionResult> RemoveFromCart(int productID)
        {
            await _productService.DeleteOrderDetail(User.GetUserId(), productID);
            return RedirectToAction("ShowShopCartOrders");
        }

        [HttpGet("RemoveCartAny/{productID}")]
        public async Task RemoveFromCartAny(int productID)
        {
            await _productService.DeleteOrderDetail(User.GetUserId(), productID);
        }

        #endregion

        #region DecreaseCountCart

        [Authorize]
        [HttpGet("DecreaseCountCart/{productID}")]
        public async Task<IActionResult> DecreaseCountOrderDetail(int productID)
        {
            await _productService.DecreaseCountOrderDetail(User.GetUserId(), productID);
            return RedirectToAction("ShowShopCartOrders");
        }

        #endregion

        #region CheckOutOrSubmit

        [Authorize]
        [HttpGet("/SubmitPrice")]
        public async Task<IActionResult> SubmitPrice()
        {
            string userName = User.GetUserName();
            return View("SubmitPrice", userName);
        }

        #endregion


        //public async Task<int> CountShopCart()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        string emailUser = User.FindFirstValue(ClaimTypes.Email);

        //        int currentUserId = _userService.GetUserIDByEmail(emailUser);

        //        var count =  await _productService.GetOrderCountByUserID(currentUserId);

        //        return count;
        //    }
        //    return 0; 
        //}

    }
}
