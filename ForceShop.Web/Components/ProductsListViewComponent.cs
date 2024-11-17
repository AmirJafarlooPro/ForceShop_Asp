using ForceShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ForceShop.Web.Components
{
    [ViewComponent]
    public class ProductsListViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductsListViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Products = await _productService.GetNewProductsAsync();
            return View(Products);
        }
    }
}
