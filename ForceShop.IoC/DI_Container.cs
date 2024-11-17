using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForceShop.Application.Services.Implementation;
using ForceShop.Application.Services.Interfaces;
using ForceShop.Data.implementation.Group;
using ForceShop.Data.implementation.Product;
using ForceShop.Data.implementation.User;
using ForceShop.Domian.Interfaces.Group;
using ForceShop.Domian.Interfaces.Product;
using ForceShop.Domian.Interfaces.User;
using Microsoft.Extensions.DependencyInjection;

namespace ForceShop.IoC
{
    public static class DI_Container
    {
        public static void RegisterService(this IServiceCollection service)
        {

            #region User

            service.AddScoped<IUserRepository, UserRepository>();

            service.AddScoped<IUserService, UserService>();

            #endregion

            #region Email

            service.AddScoped<IEmailSender, EmailSender>();

            #endregion

            #region Group

            service.AddScoped<IGroupRepository, GroupRepository>();

            service.AddScoped<IGroupService, GroupService >();

            #endregion

            #region Product

            service.AddScoped<IProductRepository,ProductRepository>();

            service.AddScoped<IProductService, ProductService>();

            #endregion

        }
    }
}
