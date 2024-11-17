using ForceShop.Application.Services.Interfaces;
using ForceShop.Data.Contex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ForceShop.Web.Components
{
    [ViewComponent]
    public class ProductGroupsViewComponent:ViewComponent
    {
        private readonly IGroupService _groupservice;

        public ProductGroupsViewComponent(IGroupService groupservice)
        {
            _groupservice = groupservice;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _groupservice.GetAllAsync();

            if (!list.IsNullOrEmpty())
            {
                return View(list);
            }

            return Content("گروهی وجود ندارد");
        }
    }
}
