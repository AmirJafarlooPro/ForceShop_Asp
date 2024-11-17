using ForceShop.Application.Services.Interfaces;
using ForceShop.Data.Contex;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ForceShop.Web.Components
{
    [ViewComponent]
    public class ProductGroupsFooterViewComponent : ViewComponent
    {
        private readonly IGroupService _groupService;
        private readonly IServiceScopeFactory _scopeFactory;

        public ProductGroupsFooterViewComponent(IGroupService groupService, IServiceScopeFactory scopeFactory)
        {
            _groupService = groupService;
            _scopeFactory = scopeFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var scopedGroupService = scope.ServiceProvider.GetRequiredService<IGroupService>();

                var list = await scopedGroupService.GetAllForFooterAsync();

                if (list != null && list.Any())
                {
                    return View(list);
                }

                return Content("گروهی وجود ندارد");
            }
        }
    }

}
