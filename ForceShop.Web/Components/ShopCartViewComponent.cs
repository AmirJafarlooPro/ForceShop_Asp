using ForceShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ForceShop.Web.Components
{
    [ViewComponent]
    public class ShopCartViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        public ShopCartViewComponent(IProductService productService, IUserService userService)
        {
            _productService = productService;
            _userService = userService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string emailUser = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            int currentUserId = _userService.GetUserIDByEmail(emailUser);

            var order = await _productService.GetOrderByUserID(currentUserId);

            if (order != null)
            {
                return View(order); // بازگشت View با داده
            }
            else
            {
                // بازگشت View خالی یا پیام پیش‌فرض
                return View("Empty"); // فرض بر این است که View با نام "Empty" موجود است
            }

        }

    }
}
